using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Scrypt;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Models.Account;

namespace SportsBetsServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        public AccountService(IConfiguration config) 
        {
            _config = config;
        }
        public string CreatePasswordHash(string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            string hashedPassword = encoder.Encode(password);
            return hashedPassword;
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            bool result = encoder.Compare(password, hashedPassword);
            return result;
        }
        public Claim[] GenerateNewUserClaim(User user)
        {
            return new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username)
            };
        }
        public Account CreateNewAccount(UserCredentials userCredentials)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Username = userCredentials.Username,
                AvailableBalance = 100,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                HashedPassword = CreatePasswordHash(userCredentials.Password)
            };
        }
        public string CreateJsonToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GenerateNewUserClaim(user)),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}