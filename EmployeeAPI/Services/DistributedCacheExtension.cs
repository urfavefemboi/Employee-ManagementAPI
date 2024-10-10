using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace EmployeeAPI.Services
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache,string recordId
            ,T Data,TimeSpan? setSlidingExpiration=null,TimeSpan? setAbsoluteExpiration = null)
        {
            //setting options
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = setAbsoluteExpiration ?? TimeSpan.FromSeconds(60),
                SlidingExpiration = setSlidingExpiration ?? TimeSpan.FromMinutes(5)
            };

            //serialize data
            var jsondata = JsonSerializer.Serialize(Data);

            //set data
            await cache.SetStringAsync(recordId, jsondata,options);
           
        }
        public static async Task<T?> GetRecordsAsync<T>(this IDistributedCache cache,string recordId)
        {
            var jsondata = await cache.GetStringAsync(recordId);
            if(jsondata is null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(jsondata);
        }
    }
}
