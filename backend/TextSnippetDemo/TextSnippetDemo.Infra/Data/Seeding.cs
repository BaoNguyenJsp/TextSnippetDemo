using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TextSnippetDemo.Infra.Data
{
    public static class Seeding
    {
        public static void SeedData(ModelBuilder builder)
        {
            builder.Entity<IdentityUser<int>>().HasData(
                new IdentityUser<int> { Id = 1, AccessFailedCount = 0, ConcurrencyStamp = "101cd6ae-a8ef-4a37-97fd-04ac2dd630e4", Email = "system@test.com", EmailConfirmed = false, LockoutEnabled = false, NormalizedEmail = "SYSTEM@TEST.COM", NormalizedUserName = "SYSTEM@TEST.COM", PasswordHash = "AQAAAAEAACcQAAAAEAEqSCV8Bpg69irmeg8N86U503jGEAYf75fBuzvL00/mr/FGEsiUqfR0rWBbBUwqtw==", PhoneNumberConfirmed = false, SecurityStamp = "a9565acb-cee6-425f-9833-419a793f5fba", TwoFactorEnabled = false, UserName = "system@simplcommerce.com" },
                new IdentityUser<int> { Id = 2, AccessFailedCount = 0, ConcurrencyStamp = "c83afcbc-312c-4589-bad7-8686bd4754c0", Email = "admin@test.com", EmailConfirmed = false, LockoutEnabled = false, NormalizedEmail = "ADMIN@TEST.COM", NormalizedUserName = "ADMIN@TEST.COM", PasswordHash = "AQAAAAEAACcQAAAAEAEqSCV8Bpg69irmeg8N86U503jGEAYf75fBuzvL00/mr/FGEsiUqfR0rWBbBUwqtw==", PhoneNumberConfirmed = false, SecurityStamp = "d6847450-47f0-4c7a-9fed-0c66234bf61f", TwoFactorEnabled = false, UserName = "admin@simplcommerce.com" }
            );

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, ConcurrencyStamp = "00d172be-03a0-4856-8b12-26d63fcf4374", Name = "user", NormalizedName = "USER" },
                new IdentityRole<int> { Id = 2, ConcurrencyStamp = "4776a1b2-dbe4-4056-82ec-8bed211d1454", Name = "admin", NormalizedName = "ADMIN" }
            );

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 },
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 }
            );
        }
    }
}
