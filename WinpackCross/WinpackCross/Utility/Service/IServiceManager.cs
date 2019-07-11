using System.Collections.Generic;
using System.Threading.Tasks;

namespace WinpackCross.Utility
{
    using PLCServiceReference;
    public interface IServiceManager<DTO> where DTO : class
    {
        Task<List<DTO>> ListeleAsync(HttpType type);
        Task<bool> EkleAsync(DTO entity);
        Task<bool> GüncelleAsync(DTO entity);
        Task<bool> SilAsync(DTO entity);
    }

    public interface IPLCServiceManager
    {
        Task<object> PropertyAsync(HttpType type, object obj, object value = null);
        Task<PLC> GetStatusAsync();
    }
    public enum HttpType
    {
        POST,
        GET
    }
}
