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
            // ��������� ����������� ��� ������� � �� � �������������� EF
            string connection = builder.Configuration.GetConnectionString("SqlServerConnection");
            services.AddDbContext<RealEstateAgencyContext>(options => options.UseSqlServer(connection));
            // ���������� �����������
            services.AddMemoryCache();

            // ���������� ��������� ������
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddControllersWithViews();
            //������������� MVC - ���������
            //services.AddControllersWithViews();
            var app = builder.Build();
            // ��������� ��������� ����������� ������
            app.UseStaticFiles();

            // ��������� ��������� ������
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