using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TextSnippetDemo.Domain.Models;

namespace TextSnippetDemo.Infra.Data
{
    public class TextSnippetDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DbSet<TextSnippet> TextSnippets { get; set; }

        public TextSnippetDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Seeding.SeedData(modelBuilder);
        }
    }
}
