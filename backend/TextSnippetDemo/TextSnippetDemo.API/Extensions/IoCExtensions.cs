using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TextSnippetDemo.Application.EventHandlers;
using TextSnippetDemo.Application.Events;
using TextSnippetDemo.Application.Services;
using TextSnippetDemo.Infra.Repositories;

namespace TextSnippetDemo.API.Extensions
{
    public static class IoCExtensions
    {
        public static void AddDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITextSnippetService, TextSnippetService>();
            services.AddScoped<INotificationHandler<TextSnippetChanged>, TextSnippetChangedHandler>();
        }
    }
}
