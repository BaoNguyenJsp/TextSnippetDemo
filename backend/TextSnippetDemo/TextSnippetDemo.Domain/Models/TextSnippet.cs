using Microsoft.EntityFrameworkCore;
using System;

namespace TextSnippetDemo.Domain.Models
{
    [Index(nameof(Content), nameof(Title))]
    public class TextSnippet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
