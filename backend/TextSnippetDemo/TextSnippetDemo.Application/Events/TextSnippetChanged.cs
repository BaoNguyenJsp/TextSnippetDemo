using MediatR;

namespace TextSnippetDemo.Application.Events
{
    public class TextSnippetChanged : INotification
    {
        public string CacheKeyPrefix { get; set; }
    }
}
