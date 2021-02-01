using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using System.Security.Claims;
using System.Collections.Generic;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAccountService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Claim[] GenerateNewClaims(Account account);
        string CreateJsonToken(Account account);
        IEnumerable<Account> GetAll();
        Account GetAccountByUsername(string username);
    }
}