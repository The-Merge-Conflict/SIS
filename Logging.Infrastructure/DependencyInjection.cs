using Logging.Application.Interfaces;
using Logging.Application.Services;
using Logging.Infrastructure.Persistence;
using Logging.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLoggingInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LoggingDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoggingConnection")));

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            return services;
        }
    }
}
