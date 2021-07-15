using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;

namespace TextSnippetDemo.Application.Services
{
    public interface IAuthService
    {
        Task<LoginDto> Login(string username, string password);
    }
}
