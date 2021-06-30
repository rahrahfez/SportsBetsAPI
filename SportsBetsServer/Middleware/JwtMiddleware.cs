using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsBetsServer.Repository;

namespace SportsBetsServer.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public JwtMiddleware(
            RequestDelegate next, 
            IConfiguration config)
        {
            _next = next;
            _config = config;
        }
        public async Task Invoke(HttpContext http, RepositoryContext context)
        {
            var token = http.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachTokenToContext(http, context, token);

            await _next(http);
        }
        private async Task AttachTokenToContext(HttpContext http, RepositoryContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "Id").Value;

                http.Items["token"] = await context.Account.FindAsync(new Guid(accountId));
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"{ ex.Message }");
            }
        }
    }
}
