using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Constants;
using TextSnippetDemo.Application.Dtos;
using TextSnippetDemo.Application.Events;
using TextSnippetDemo.Application.ViewModels;
using TextSnippetDemo.Domain.Models;
using TextSnippetDemo.Infra.Extensions;
using TextSnippetDemo.Infra.Repositories;

namespace TextSnippetDemo.Application.Services
{
    public class TextSnippetService : ITextSnippetService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMediator _mediator;
        private readonly IRepository<TextSnippet> _repository;

        public TextSnippetService(IDistributedCache distributedCache, IMediator mediator, IRepository<TextSnippet> repository)
        {
            _distributedCache = distributedCache;
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<Pagination<TextSnippetDto>> GetPagination(string query, int pageSize, int pageNumber)
        {
            query ??= string.Empty;
            var base64Query = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(query));
            var cacheKey = $"{CacheKeyConstants.TextSnippetPaginationPrefix}-{pageNumber}-{pageSize}-{base64Query}";
            var ids = await _distributedCache.GetCacheDataAsync<IEnumerable<int>>(cacheKey, async () =>
            {
                var data = await _repository.Query(x => x.Title.Contains(query))
                                        .OrderBy(x => x.Title)
                                        .AsNoTracking()
                                        .Select(x => x.Id)
                                        .ToListAsync();
                return data;
            }, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            var total = ids.Count();
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

        public async Task<TextSnippetDto> Create(TextSnippetViewModel data)
        {
            var snippet = new TextSnippet
            {
                Title = data.Title,
                Content = data.Content
            };
            _repository.Add(snippet);
            await _repository.SaveChangesAsync();
            await _mediator.Publish(new TextSnippetChanged
            {
                CacheKeyPrefix = CacheKeyConstants.TextSnippetPaginationPrefix
            });
            return new TextSnippetDto
            {
                Id = snippet.Id,
                Title = snippet.Title,
                Content = snippet.Content
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
            await _mediator.Publish(new TextSnippetChanged
            {
                CacheKeyPrefix = CacheKeyConstants.TextSnippetPaginationPrefix
            });
        }

        public async Task Delete(int id)
        {
            var snippet = await _repository.Query(x => x.Id == id).FirstOrDefaultAsync();
            if (snippet == null) return;

            _repository.Remove(snippet);
            await _repository.SaveChangesAsync();
            await _mediator.Publish(new TextSnippetChanged
            {
                CacheKeyPrefix = CacheKeyConstants.TextSnippetPaginationPrefix
            });
        }
    }
}
