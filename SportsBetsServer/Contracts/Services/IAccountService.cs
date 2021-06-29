using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAccountService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Claim[] GenerateNewUserClaim(User user);
        Account CreateNewAccount(UserCredentials userCredentials);
        User Authenticate(Account account);
        string CreateJsonToken(User user);
        Task<Account> GetAccountById(Guid id);
        Task DeleteUserById(Guid id);
        Task<User> RegisterNewAccount(UserCredentials userCredentials);
        Account GetAccountByUsername(string username);
        List<User> GetAllUsers();
    }
}