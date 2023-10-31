using Microsoft.EntityFrameworkCore;
using RealEstateAgency4.Models;
namespace RealEstateAgency4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            // внедрение зависимости для доступа к БД с использованием EF
            string connection = builder.Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<RealEstateAgencyContext>(options => options.UseSqlServer(connection));
            // добавление кэширования
            services.AddMemoryCache();

            // добавление поддержки сессии
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();
            //Использование MVC - отключено
            //services.AddControllersWithViews();
            var app = builder.Build();
            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем поддержку сессий
            app.UseSession();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); ;


            app.Run();
        }
    }
}