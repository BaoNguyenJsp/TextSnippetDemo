using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.ViewModels;

namespace TextSnippetDemo.Application.Services
{
    public interface ITextSnippetService
    {
        Task<Pagination<TextSnippetDto>> GetPagination(string query, int pageSize, int pageNumber);
        Task<TextSnippetDto> Create(TextSnippetViewModel data);
        Task Update(int id, TextSnippetViewModel data);
        Task Delete(int id);
    }
}
