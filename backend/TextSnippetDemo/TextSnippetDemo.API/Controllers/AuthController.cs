using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.Services;
using TextSnippetDemo.Application.ViewModels;

namespace TextSnippetDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // This is a simple implementation of role-based authorization
        [HttpPost]
        public async Task<ActionResult<LoginDto>> Login([FromBody] LoginViewModel loginDto)
        {
            var result = await _authService.Login(loginDto.Username, loginDto.Password);
            return result;
        }
    }
}
