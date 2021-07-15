using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Events;
using TextSnippetDemo.Domain.Models;
using TextSnippetDemo.Infra.Repositories;

namespace TextSnippetDemo.Application.EventHandlers
{
    public class TextSnippetChangedHandler : INotificationHandler<TextSnippetChanged>
    {
        private readonly IRepository<CacheData> _repository;
        private readonly IDistributedCache _distributedCache;

        public TextSnippetChangedHandler(IRepository<CacheData> repository, IDistributedCache distributedCache)
        {
            _repository = repository;
            _distributedCache = distributedCache;
        }

        public async Task Handle(TextSnippetChanged notification, CancellationToken cancellationToken)
        {
            var cacheKeys = await _repository.Query(x => x.Id.StartsWith(notification.CacheKeyPrefix))
                                             .Select(x => x.Id)
                                             .ToListAsync(cancellationToken: cancellationToken);

            await Task.WhenAll(cacheKeys.Select(cacheKey => Task.Run(() => _distributedCache.RemoveAsync(cacheKey))));
        }
    }
}
