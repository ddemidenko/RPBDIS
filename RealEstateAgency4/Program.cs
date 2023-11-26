using Microsoft.EntityFrameworkCore;
using RealEstateAgency4.Middleware;
using RealEstateAgency4.Models;
using Microsoft.AspNetCore.Identity;
namespace RealEstateAgency4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("SqlServerConnection")!;

            builder.Services.AddDbContext<RealEstateAgencyContext>(options => options.UseSqlServer(connection));

            builder.Services
            .AddDefaultIdentity<IdentityUser>()
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<RealEstateAgencyContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
