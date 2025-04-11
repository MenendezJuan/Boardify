using Boardify.Application.Interfaces;
using Boardify.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boardify.Persistence.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbContextMySqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("TrelloMySqlConn")!;
            services.AddDbContext<DatabaseService>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                     mysqlOptions => mysqlOptions.CommandTimeout(180))
                );
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            return services;
        }
    }
}