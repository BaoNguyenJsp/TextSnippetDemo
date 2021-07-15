using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.ViewModels;
using TextSnippetDemo.Domain.Models;
using TextSnippetDemo.Infra.Repositories;

namespace TextSnippetDemo.Application.Services
{
    public class TextSnippetService : ITextSnippetService
    {
        private readonly IRepository<TextSnippet> _repository;

        public TextSnippetService(IRepository<TextSnippet> repository)
        {
            _repository = repository;
        }

        public async Task<Pagination<TextSnippetDto>> GetPagination(string query, int pageSize, int pageNumber)
        {
            query ??= string.Empty;
            var ids = await _repository.Query(x => x.Title.Contains(query))
                                       .OrderBy(x => x.Title)
                                       .AsNoTracking()
                                       .Select(x => x.Id)
                                       .ToListAsync();

            var total = ids.Count;
            var pagingIds = ids.Skip(pageSize * pageNumber).Take(pageSize);

            var data = await _repository.Query(x => pagingIds.Contains(x.Id))
                                        .Select(x => new TextSnippetDto
                                        {
                                            Id = x.Id,
                                            Title = x.Title,
                                            Content = x.Content
                                        })
                                        .AsNoTracking()
                                        .ToListAsync();

            return new Pagination<TextSnippetDto>
            {
                Total = total,
                Data = data
            };
        }

        public async Task Update(int id, TextSnippetViewModel data)
        {
            var snippet = await _repository.Query(x => x.Id == id).FirstOrDefaultAsync();
            if (snippet == null) return;

            snippet.Title = data.Title;
            snippet.Content = data.Content;
            _repository.Update(snippet);
            await _repository.SaveChangesAsync();
        }

        public async Task Create(TextSnippetViewModel data)
        {
            var snippet = new TextSnippet
            {
                Title = data.Title,
                Content = data.Content
            };
            _repository.Add(snippet);
            await _repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var snippet = await _repository.Query(x => x.Id == id).FirstOrDefaultAsync();
            if (snippet == null) return;

            _repository.Remove(snippet);
            await _repository.SaveChangesAsync();
        }
    }
}
