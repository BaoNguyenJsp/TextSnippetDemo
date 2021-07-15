using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TextSnippetDemo.API.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static void AddCustomCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDistributedSqlServerCache(options =>
            {
                var cacheSetting = configuration.GetSection("CacheSetting");
                options.ConnectionString = cacheSetting["DistributedCacheConnection"];
                options.SchemaName = cacheSetting["SchemaName"];
                options.TableName = cacheSetting["TableName"];
            });
        }
    }
}
