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
        Claim[] GenerateNewUserClaim(User user);
        Account CreateNewAccount(UserCredentials userCredentials);
        string CreateJsonToken(User user);
    }
}