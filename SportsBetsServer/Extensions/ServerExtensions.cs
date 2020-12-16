using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Contracts.Services;
using LoggerService;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Repository;
using SportsBetsServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {});
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
        public static void ConfigureRepositoryBase(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
        }
        public static void ConfigureAuthService(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
        }
        public static void ConfigureUserService(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
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