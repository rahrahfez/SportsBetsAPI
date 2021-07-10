using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
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
using System.Threading.Tasks;

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
        public bool VerifyPassword(string password, string hashedPassword)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            bool result = encoder.Compare(password, hashedPassword);
            return result;
        }
        public string CreatePasswordHash(string password)
        {
            ScryptEncoder encoder = new ScryptEncoder();
            string hashedPassword = encoder.Encode(password);
            return hashedPassword;
        }

        public Claim[] GenerateNewUserClaim(User user)
        {
            return new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Username", user.Username)
            };
        }
        public Account Authenticate(UserCredentials userCredentials)
        {
            var account = GetAccountByUsername(userCredentials.Username);
            if (!VerifyPassword(userCredentials.Password, account.HashedPassword))
            {
                return null;
            }
            return account;
        }
        public async Task<Account> RegisterNewAccount(UserCredentials userCredentials)
        {
            var account = GetAccountByUsername(userCredentials.Username);
            if (account != null)
            {
                throw new AppException("Username already exists.");
            }

            var newAccount = CreateNewAccount(userCredentials);

            await _context.Account.AddAsync(newAccount);
            await _context.SaveChangesAsync();

            return newAccount;
        }
        public Account CreateNewAccount(UserCredentials userCredentials)
        {
            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Username = userCredentials.Username,
                AvailableBalance = 100,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow,
                HashedPassword = CreatePasswordHash(userCredentials.Password)
            };
            _logger.LogInfo($"New account { newAccount.Username }, created at { newAccount.CreatedAt }.");
            return newAccount;
        }
        public User MapAccountToUser(Account account)
        {
            return _mapper.Map<User>(account);
        }
        public async Task<Account> GetAccountById(Guid id)
        {
            var account = await _context.Account.FindAsync(id);

            if (account.Id.Equals(Guid.Empty) || account == null) throw new NotFoundException("Account not found.");

            return account;
        }
        public Account GetAccountByUsername(string username)
        {
            var account = _context.Account.Where(x => x.Username.Equals(username)).SingleOrDefault();
            return account;
        }
        public List<User> GetAllUsers()
        {
            var accounts = _context.Account.ToList();

            if (accounts == null)
                throw new AppException("Account list is empty");

            var users = new List<User>();
            foreach (var account in accounts)
            {
                var user = _mapper.Map<User>(account);
                users.Add(user);
            }
            return users;
        }
        public async Task DeleteUserById(Guid id)
        {
            var userToBeDeleted = await _context.Account.FindAsync(id);

            if (userToBeDeleted != null)
            {
                _context.Account.Remove(userToBeDeleted);
                await _context.SaveChangesAsync();
            }
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