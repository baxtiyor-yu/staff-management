using Microsoft.Extensions.DependencyInjection;
using Synel_staff.Application.Common;
using Synel_staff.Application.EmployeeInterfaces;
using Synel_staff.Application.EmployeeServices;

namespace Synel_staff.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<CsvService>();

            return services;
        }
    }
}
