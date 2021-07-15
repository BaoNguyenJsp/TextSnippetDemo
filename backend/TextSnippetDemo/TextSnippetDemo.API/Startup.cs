using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TextSnippetDemo.API.Extensions;

namespace TextSnippetDemo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Use SQL Server as database. The migration will happen in Infra project
            services.AddDatabase(Configuration);
            // Add Identity for authentication and custom policy for role-based authorization
            services.AddCustomAuthentication(Configuration);
            // Add caching to improve performance in searching
            services.AddCustomCache(Configuration);
            // Add MediatR to make the structure loosely coupling between main domain and caching
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // IoC registration
            services.AddDI();
            // Add global exception filter to handle error globally
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpExceptionGlobalFilter));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
