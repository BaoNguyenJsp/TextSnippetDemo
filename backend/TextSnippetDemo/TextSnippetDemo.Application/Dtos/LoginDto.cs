using System.Collections.Generic;

namespace TextSnippetDemo.Application.Dtos
{
    public class LoginDto
    {
        public string JwtToken { get; set; }
        // For role-based in Angular
        public IEnumerable<string> Roles { get; set; }
    }
}
