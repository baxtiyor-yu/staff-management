
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Infrastructure.Persistance;
using Synel_staff.Infrastructure.Repositories;

namespace Synel_staff.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(conf.GetConnectionString("DefaultConn"));
            });
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();

            return services;
        }
    }
}
