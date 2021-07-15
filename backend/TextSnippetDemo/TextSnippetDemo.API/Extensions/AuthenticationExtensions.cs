using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using TextSnippetDemo.Infra.Data;

namespace TextSnippetDemo.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity();
            services.AddJwt(configuration);
            services.AddCustomPolicy();
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<TextSnippetDbContext>()
                .AddUserManager<UserManager<IdentityUser<int>>>()
                .AddDefaultTokenProviders();
        }

        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(c =>
                {
                    var jwtOptions = configuration.GetSection("JwtOptions");
                    var securityKey = Encoding.UTF8.GetBytes(jwtOptions["JwtKey"]);

                    c.RequireHttpsMetadata = false;
                    c.ClaimsIssuer = jwtOptions["Issuer"];
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtOptions["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(securityKey),
                        RequireExpirationTime = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );
        }

        public static void AddCustomPolicy(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ADMIN", policy =>
                {
                    policy.RequireAssertion(ctx =>
                    {
                        var role = ctx.User.Claims.Where(x => x.Type == "Role");
                        return role.Any(x => x.Value == "admin");
                    });
                });
            });
        }
    }
}
