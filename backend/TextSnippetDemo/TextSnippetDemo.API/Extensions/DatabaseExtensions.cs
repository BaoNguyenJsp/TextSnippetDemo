using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextSnippetDemo.Infra.Data;

namespace TextSnippetDemo.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TextSnippetDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("TextSnippetDemo.Infra")));
        }
    }
}
