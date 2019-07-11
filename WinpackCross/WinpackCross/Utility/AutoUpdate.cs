using Independentsoft.Webdav;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace WinpackCross.Utility
{
    public static class AutoUpdate
    {
        public delegate void DownloadHandler(long MaxLength, ProgressEventArgs e);
        public delegate void DownloadCompHandler(string FilePath);
        public static event DownloadHandler DownloadProgress;
        public static event DownloadCompHandler DownloadComplated;

        static string adress = "https://webdav.yandex.com.tr/Apk";
        static string ConfigFile = "config.json";

        static Resource resource = new Resource(new WebdavSession(new NetworkCredential("soulbox28", "kdr63792958")));
        static PropertyName[] propame = new PropertyName[] {
                 DavProperty.DisplayName,
                 DavProperty.CreationDate,
                 DavProperty.GetLastModified,
                 DavProperty.GetContentLength
             };

        public class config
        {
            [JsonProperty]
            public string version { get; set; }
        }

        public static bool isUpdate
        {
            get
            {
                Stream stream = resource.GetInputStream(Path.Combine(adress, ConfigFile));
                using (StreamReader sread = new StreamReader(stream))
                {
                    var vers = JsonConvert.DeserializeObject<config>(sread.ReadToEnd());
                    if (vers.version == DependencyService.Get<IUtility>().GetBuildNumber())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static string DownloadFileName
        {
            get
            {
                ResourceInfo[] infos = resource.List(adress, propame);
                return infos
                    .FirstOrDefault(x => x.Properties[DavProperty.DisplayName].Value.Contains(".apk"))
                    .Properties[DavProperty.DisplayName].Value;
            }
        }
        static long itemLong = 0;
        static string downloadpath;
        public static void Download()
        {
            if (!isUpdate)
            {
                ResourceInfo[] infos = resource.List(adress, propame);
                var info = infos.FirstOrDefault(x => x.Properties[DavProperty.DisplayName].Value.Contains(".apk"));
                downloadpath = Path.Combine(DependencyService.Get<IUtility>().Path, info.Properties[DavProperty.DisplayName].Value);
                resource.DownloadProgress += Resource_DownloadProgress;
                itemLong = info.Properties[DavProperty.GetContentLength].Value.ToLong();
                resource.DownloadFile(info.Address, downloadpath);
            }
        }
        private static void Resource_DownloadProgress(object sender, ProgressEventArgs e)
        {
            try
            {

                DownloadProgress?.Invoke(itemLong, e);
                if (e.IsComplete)
                {

                    DownloadComplated?.Invoke(downloadpath);
                    resource.DownloadProgress -= Resource_DownloadProgress;
                }
            }
            catch (Exception)
            {

                resource.DownloadProgress -= Resource_DownloadProgress;
            }
        }

        public static void UpdateVersion()
        {
            string Version = DependencyService.Get<IUtility>().GetBuildNumber();
            string contents = JsonConvert.SerializeObject(new config()
            {
                version = Version
            });
            string FileEnd = "config.json";
            string filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), FileEnd);
            //resource.UploadProgress += Resource_UploadProgress;
            File.WriteAllText(filename, contents);

            FileInfo file = new FileInfo(filename);

            if (System.IO.File.Exists(filename))
            {
                Console.WriteLine("var");
            }

            resource.Upload(Path.Combine(adress, FileEnd), filename);

        }

    }
}
