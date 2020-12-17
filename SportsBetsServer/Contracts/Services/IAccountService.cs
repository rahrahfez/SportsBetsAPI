using SportsBetsServer.Models.Account;
using System.Security.Claims;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAccountService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Claim[] GenerateNewClaims(User user);
        string CreateJsonToken(User user);
    }
}