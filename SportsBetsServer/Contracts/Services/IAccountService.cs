using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;
using System.Security.Claims;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAccountService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Claim[] GenerateNewUserClaim(User user);
        Account CreateNewAccount(UserCredentials userCredentials);
        User Authenticate(UserCredentials userCredentials);
        string CreateJsonToken(User user);
    }
}