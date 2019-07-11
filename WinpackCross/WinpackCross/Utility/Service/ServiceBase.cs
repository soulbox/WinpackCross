using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WinpackCross.Utility
{



    public partial class ServiceBase
    {
        public enum ClientUri
        {
            UserApi,
            UserPerApi,
            MachineApi,
            PLCApi
        }
        public static HttpClient Client;
        public static HttpRequestMessage request;
        public static StringContent content;
        private static Dictionary<ClientUri, string> Urls = new Dictionary<ClientUri, string>()
        {
            { ClientUri.UserApi,        "http://localhost:9005/User" },
            { ClientUri.MachineApi ,    "http://localhost:9005/Machine" },
            { ClientUri.PLCApi ,        "http://localhost:9005/asansorisiklarisinem" },
            { ClientUri.UserPerApi ,    "http://localhost:9005/UserPermission" }


        };

        public const string mediatype = "application/json";
        const string LocalIP = "192.168.1.190";

        //private string GetIP => Ping.PingHost(LocalIP, 1433) ? LocalIP : DbModel.DBGlobal.GetGlobalIP.İp;
        //private string GetIP => Ping.PingHost(LocalIP, 1433) ? Ping.PingHost(LocalIP, 9005) ? LocalIP : "192.168.1.115" : DbModel.DBGlobal.GetGlobalApi.İp;

        private string GetIP
        {
            get
            {
                string dön= "";
                if (Ping.PingHost("192.168.1.115", 9005))
                    return dön = "192.168.1.115";
                if (Ping.PingHost(LocalIP, 9005))
                    return dön = LocalIP;
                else
                    return dön = DbModel.DBGlobal.GetGlobalApi.İp;
                
            }
        }

        //private string GetIP => "192.168.1.115";



        Uri _uri;
        public string url = "";
        public ServiceBase(ClientUri uri)
        {
            Client = Client ?? new HttpClient();
            request = request ?? new HttpRequestMessage() { Method = HttpMethod.Get };
            content = content ?? new StringContent("");
            url = Urls[uri].Replace("localhost", GetIP);
            _uri = _uri ?? new Uri(url);
            Client.DefaultRequestHeaders.Add("Accept", mediatype);

        }
        public HttpRequestMessage GetRequest(string urlstr, string strcontent)
        {
            request.RequestUri = _uri = new Uri(urlstr);
            request.Content = GetContent(strcontent);
            return request;
        }
        public StringContent GetContent(string strcontent)
        {
            content = new StringContent(strcontent, Encoding.UTF8, mediatype);
            return content;
        }

    }
}
