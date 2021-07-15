using System;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.ViewModels;

namespace TextSnippetDemo.Application.Services
{
    public class TextSnippetService : ITextSnippetService
    {
        public Task<Pagination<TextSnippetDto>> GetPagination(string query, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task Create(TextSnippetViewModel data)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, TextSnippetViewModel data)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
