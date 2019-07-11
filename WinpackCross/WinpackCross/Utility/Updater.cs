using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebDav;
using Xamarin.Forms;


namespace WinpackCross.Utility
{
    public class Updater
    {

        class config
        {
            [JsonProperty]
            public string version { get; set; }
        }




        static string adress = "https://webdav.yandex.com.tr/Apk";
        static string ConfigFile = "config.json";
        // Handle when your app start
        static WebDavClientParams clientParams = new WebDavClientParams
        {
            BaseAddress = new Uri(adress),
            Credentials = new NetworkCredential("soulbox28", "kdr63792958")
        };
        static string downloadpath;
        static bool _isupdate = false;
        static WebClient cl = new WebClient()
        {
            UseDefaultCredentials = true,
            Credentials = clientParams.Credentials
        };
        static WebDavClient client = new WebDavClient(clientParams);

        public delegate void DownloadHandler(long MaxLength,DownloadProgressChangedEventArgs e);
        public delegate void DownloadCompHandler(string FilePath);
        public static event DownloadHandler DownloadProgress;
        public static event DownloadCompHandler DownloadComplated;




        public static async Task<bool> IsUpdateAsync()
        {
            if (!CrossConnectivity.Current.IsConnected) return true;

            var result = await client.Propfind("");
            foreach (var item in result.Resources)
            {
                if (item.DisplayName.Contains(".json"))
                {
                    var conf = await client.GetProcessedFile(adress + "/" + item.DisplayName);
                    using (var stream = new StreamReader(conf.Stream))
                    {
                        var vers = JsonConvert.DeserializeObject<config>(stream.ReadToEnd());
                        if (vers.version == DependencyService.Get<IUtility>().GetBuildNumber())
                        {
                            _isupdate = true;
                        }
                        else
                        {
                            _isupdate = false;
                        }
                    }
                }
            }
            return _isupdate;
        }

        static long itemLong = 0;
        public static async void Download()
        {
            if (!CrossConnectivity.Current.IsConnected) return;
            if (!_isupdate)
            {

                var result = await client.Propfind("");
                foreach (var item in result.Resources)
                {
                    if (item.DisplayName.Contains(".apk"))
                    {
                        itemLong = (long)item.ContentLength;
                        downloadpath = Path.Combine(DependencyService.Get<IUtility>().Path, item.DisplayName);
                        cl.DownloadProgressChanged += Cl_DownloadProgressChanged;       
                        cl.DownloadFileAsync(new Uri(adress + "/" + item.DisplayName), downloadpath);
                    }
                }
            }
        }

        private static void Cl_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                double bytesIn = e.BytesReceived;
                double totalBytes = itemLong;
                double percentage = bytesIn / totalBytes * 100; 
                Console.WriteLine("Downloaded " + e.BytesReceived + " of " + itemLong);
                Console.WriteLine("% " + percentage);
                DownloadProgress?.Invoke(itemLong,e);
                if (e.BytesReceived==itemLong)
                {
                    DownloadComplated?.Invoke(downloadpath);
                    cl.DownloadProgressChanged -= Cl_DownloadProgressChanged;

                }
            }
            catch (Exception )
            {

                cl.DownloadProgressChanged -= Cl_DownloadProgressChanged;

            }
        }


    }
}
