using System;

namespace TextSnippetDemo.Domain.Models
{
    public class CacheData
    {
        public string Id { get; set; }
        public byte[] Value { get; set; }
        public DateTimeOffset? ExpiresAtTime { get; set; }
        public long? SlidingExpirationInSeconds { get; set; }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}
