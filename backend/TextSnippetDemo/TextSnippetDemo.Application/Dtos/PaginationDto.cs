using System.Collections.Generic;

namespace TextSnippetDemo.Application.Dtos
{
    public class Pagination<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
