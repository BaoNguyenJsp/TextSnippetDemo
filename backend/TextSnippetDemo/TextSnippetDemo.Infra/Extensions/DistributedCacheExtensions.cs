using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace TextSnippetDemo.Infra.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task<T> GetCacheDataAsync<T>(this IDistributedCache distributedCache
               , string key, Func<Task<T>> acquire
               , DistributedCacheEntryOptions distributedCacheEntryOptions = null
               , bool isEnableCache = true)
        {
            var result = !isEnableCache ? default : await distributedCache.GetAsync(key);
            if (result == null)
            {
                var data = await acquire();
                result = ToByteArray<T>(data);

                if (distributedCacheEntryOptions != null)
                {
                    await distributedCache.SetAsync(key, result, distributedCacheEntryOptions);
                }
                else
                {
                    await distributedCache.SetAsync(key, result);
                }
            }

            return FromByteArray<T>(result);
        }
        public static byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default;
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data));
        }
    }
}
