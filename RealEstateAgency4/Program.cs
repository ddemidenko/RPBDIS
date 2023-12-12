using Microsoft.EntityFrameworkCore;
using RealEstateAgency4.Middleware;
using Microsoft.AspNetCore.Identity;
using RealEstateAgency4.Services;
using RealEstateAgency4.Models;

namespace RealEstateAgency4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("SqlServerConnection")!;

            builder.Services.AddDbContext<RealEstateAgencyContext>(options => options.UseSqlServer(connection));

            builder.Services.AddTransient<ContractsCache>();
            builder.Services.AddTransient<SellersCache>();
            builder.Services.AddTransient<ApartmentsCache>();
            builder.Services.AddTransient<ServicesCache>();

            builder.Services.AddMemoryCache();

            builder.Services
            .AddDefaultIdentity<IdentityUser>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RealEstateAgencyContext>();


            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseDBInitializer();
                        app.UseAuthentication();;

            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
