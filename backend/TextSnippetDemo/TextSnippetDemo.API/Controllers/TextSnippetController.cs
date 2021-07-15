using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.Services;
using TextSnippetDemo.Application.ViewModels;

namespace TextSnippetDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextSnippetController : ControllerBase
    {
        private readonly ITextSnippetService _service;

        public TextSnippetController(ITextSnippetService service)
        {
            _service = service;
        }

        [HttpGet("pagination")]
        public async Task<Pagination<TextSnippetDto>> GetPagination([FromQuery] string query, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var results = await _service.GetPagination(query, pageSize, pageNumber);
            return results;
        }

        [HttpPost]
        [Authorize(Policy = "ADMIN")]
        public async Task Create([FromBody] TextSnippetViewModel data)
        {
            await _service.Create(data);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ADMIN")]
        public async Task Put([FromRoute] int id, [FromBody] TextSnippetViewModel data)
        {
            await _service.Update(id, data);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ADMIN")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
    }
}
