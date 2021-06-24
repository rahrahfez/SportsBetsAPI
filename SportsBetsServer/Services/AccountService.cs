using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using LoggerService;
using Scrypt;
using SportsBetsServer.Repository;
using SportsBetsServer.Entities;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Helpers;

namespace SportsBetsServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly RepositoryContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AccountService(
            RepositoryContext context,
            IMapper mapper,
            ILoggerManager logger,
            IConfiguration config) 
        {
            _config = config;
            _mapper = mapper;
            _logger = logger;
            _context = context;
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
        public User Authenticate(UserCredentials userCredentials)
        {
            var account = _context.Account.Where(x => x.Username.Equals(userCredentials.Username)).Single();

            if (!VerifyPassword(userCredentials.Password, account.HashedPassword))
            {
                throw new AppException("Incorrect Username and/or Password.");
            }

            var authenticatedUser = _mapper.Map<User>(account);
            var token = CreateJsonToken(authenticatedUser);
            authenticatedUser.Token = token;

            return authenticatedUser;
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