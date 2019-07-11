using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WinpackCross.Utility
{
    using Newtonsoft.Json;
    using System.Reflection;
    using PLCServiceReference;


    public class ServiceManager<DTO> : ServiceBase, IServiceManager<DTO>
        where DTO : class
    {

        public ServiceManager(ClientUri uri) : base(uri)
        {

        }

        public async Task<List<DTO>> ListeleAsync(HttpType type)
        {
            string Methodname = MethodBase.GetCurrentMethod().ReflectedType.Name.Replace("<", "");
            int index = Methodname.IndexOf('>');
            Methodname = Methodname.Substring(0, index).Replace("Async", "");
            string posturl = $"{url}/{Methodname}";
            string Responsestr = "";
            if (type == HttpType.POST)
            {
                var response = await Client.PostAsync(posturl, GetContent(""));
                Responsestr = await response.Content.ReadAsStringAsync();
            }
            else if (type == HttpType.GET)
            {
                Responsestr = await Client.GetStringAsync(posturl);
            }
            return JsonConvert.DeserializeObject<List<DTO>>(Responsestr);
        }



        public Task<bool> EkleAsync(DTO entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> GüncelleAsync(DTO entity)
        {
            throw new NotImplementedException();
        }
        public Task<bool> SilAsync(DTO entity)
        {
            throw new NotImplementedException();
        }


    }


}
