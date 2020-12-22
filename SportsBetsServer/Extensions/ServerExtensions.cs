using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LoggerService;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities;
using SportsBetsServer.Repository;
using SportsBetsServer.Services;

namespace SportsBetsServer.Extensions
{
    public static class ServerExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
                    //.AllowCredentials());
            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureMySql(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];

            services.AddDbContext<RepositoryContext>(options => 
                options.UseMySql(connectionString));
        }
        public static void ConfigureAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }         
    }
}