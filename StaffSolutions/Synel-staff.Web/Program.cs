using Synel_staff.Infrastructure;
using Synel_staff.Application;
using Synel_staff.Infrastructure.Extensions;

namespace Synel_staff.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddInfrastructureDI(builder.Configuration);

            builder.Services.AddApplicationDI();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {               
                app.UseHsts();
            }

            app.UseExceptionHandler("/Employee/Error");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Employee}/{action=Index}/{id?}");
            //Create database and table if not exists
            app.CreateDbIfNotExists();

            app.Run();
        }
    }
}
