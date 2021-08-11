using System;
using System.Linq;
using System.Collections.Generic;
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

        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public AccountService(
            RepositoryContext context,
            IMapper mapper,
            ILoggerManager logger) 
        {
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
        public Account Authenticate(AccountRequestDTO userCredentials)
        {
            var account = GetAccountByUsername(userCredentials.Username);
            if (!VerifyPassword(userCredentials.Password, account.HashedPassword))
            {
                return null;
            }
            return account;
        }
        public async Task<Account> RegisterNewAccount(AccountRequestDTO userCredentials)
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
        public Account CreateNewAccount(AccountRequestDTO userCredentials)
        {
            var Id = Guid.NewGuid();
            var AvailableBalance = 100;
            var HashedPassword = CreatePasswordHash(userCredentials.Password);
            var newAccount = new Account(
                Id, 
                userCredentials.Username, 
                HashedPassword, 
                AvailableBalance, 
                DateTime.Now, 
                DateTime.Now, 
                DateTime.Now)
            {};
            _logger.LogInfo($"New account { newAccount.Username }, created at { newAccount.CreatedAt }.");
            return newAccount;
        }
        public AccountResponseDTO MapAccountToUser(Account account)
        {
            return _mapper.Map<AccountResponseDTO>(account);
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
        public List<AccountResponseDTO> GetAllUsers()
        {
            var accounts = _context.Account.ToList();

            if (accounts == null)
                throw new AppException("Account list is empty");

            var users = new List<AccountResponseDTO>();
            foreach (var account in accounts)
            {
                var user = _mapper.Map<AccountResponseDTO>(account);
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
    }
}