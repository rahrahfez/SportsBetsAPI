using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities;
using System.Security.Claims;

namespace SportsBetsServer.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRepositoryBase<Account> _repo;
        private readonly IConfiguration _config;
        public JwtMiddleware(
            RequestDelegate next, 
            IRepositoryBase<Account> repo,
            IConfiguration config)
        {
            _next = next;
            _repo = repo;
            _config = config;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachTokenToContext(context, token);

            await _next(context);
        }
        private async Task AttachTokenToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

                context.Items["Account"] = await _repo.GetAsync(new Guid(accountId));
            }
            catch { }
        }
    }
}
