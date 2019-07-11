using System;
using System.Collections.Generic;
using System.Text;

namespace WinpackCross.Utility
{
    using Newtonsoft.Json;
    using PLCServiceReference;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    public class PLCServiceManager : ServiceBase
    {
        public PLCServiceManager(ClientUri uri) : base(uri)
        {

        }


        sendPost Post = new sendPost();
        public async Task<PLC> GetStatusAsync()
        {
            string Methodname = MethodBase.GetCurrentMethod().ReflectedType.Name.Replace("<", "");
            int index = Methodname.IndexOf('>');
            Methodname = Methodname.Substring(0, index).Replace("Async", "");
            string posturl = $"{url}/{Methodname}";
            string Responsestr = await Client.GetStringAsync(posturl);
            return JsonConvert.DeserializeObject<PLC>(Responsestr);
        }
        public async Task<object> GetPropValueAsync(string name)
        {
            string Methodname = MethodBase.GetCurrentMethod().ReflectedType.Name.Replace("<", "");
            int index = Methodname.IndexOf('>');
            Methodname = Methodname.Substring(0, index).Replace("Async", "");
            string posturl = $"{url}/{Methodname}?name={name}";
            return JsonConvert.DeserializeObject<object>(await Client.GetStringAsync(posturl));
        }
        public async Task<PostResult> SetPropValueAsync(string name, object value)
        {
            string Methodname = MethodBase.GetCurrentMethod().ReflectedType.Name.Replace("<", "");
            int index = Methodname.IndexOf('>');
            Methodname = Methodname.Substring(0, index).Replace("Async", "");
            string posturl = $"{url}/{Methodname}";
            Post.name = name;
            Post.value = value;
            var response = await Client.PostAsync(posturl, GetContent(JsonConvert.SerializeObject(Post)));
            var result = await response.Content.ReadAsStringAsync();
            var sonuc = JsonConvert.DeserializeObject<PostResult>(result);
            return sonuc;
        }
        private  async Task<object> PropertyAsync(HttpType type, object obj, object value = null)
        {
            string jsonstr = JsonConvert.SerializeObject(obj);
            string Methodname = MethodBase.GetCurrentMethod().ReflectedType.Name.Replace("<", "");
            int index = Methodname.IndexOf('>');
            Methodname = Methodname.Substring(0, index).Replace("Async", "");
            string posturl = $"{url}/{Methodname}";
            string Responsestr = "";
            if (type == HttpType.GET)
            {
                var response = await Client.SendAsync(GetRequest(posturl, jsonstr)).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                Responsestr = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else if (type == HttpType.POST)
            {
                content = new StringContent(jsonstr, Encoding.UTF8, mediatype);
                var response = await Client.PostAsync(posturl, content);
                Responsestr = await response.Content.ReadAsStringAsync();
            }
            return JsonConvert.DeserializeObject<object>(Responsestr);
        }
        class sendPost
        {
            public string name { get; set; }
            public object value { get; set; }
        }
        public class PostResult
        {
            public object SetPropValueResult { get; set; }
        }
    }
}
