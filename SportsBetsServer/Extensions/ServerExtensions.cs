using Microsoft.Extensions.DependencyInjection;
using SportsBetsServer.Contracts.Services;
using LoggerService;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Repository;
using SportsBetsServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

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
                    .AllowAnyMethod()
                    .AllowCredentials());
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
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policy.Admin, Policy.AdminPolicy());
                options.AddPolicy(Policy.User, Policy.UserPolicy());
                options.FallbackPolicy = Policy.FallBackPolicy();
            });
        }
            
    }
}