using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RealEstateAgency4.Models;
using System.Threading.Tasks;

namespace RealEstateAgency4.Middleware
{
    public class DBInitializer
    {
        private readonly RequestDelegate _next;
        public DBInitializer(RequestDelegate next) => _next = next;

        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, RealEstateAgencyContext dbContext, UserManager<IdentityUser> userManager)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DataInitializer.Initialize(dbContext, userManager);
                context.Session.SetString("starting", "Yes");
            }
            return _next.Invoke(context);
        }
    }

    public static class DBInitializerExtensions
    {
        public static IApplicationBuilder UseDBInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DBInitializer>();
        }
    }

}
