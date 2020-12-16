using SportsBetsServer.Models.Account;
using System.Security.Claims;

namespace SportsBetsServer.Contracts.Services 
{
    public interface IAuthService 
    {
        string CreatePasswordHash(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Claim[] GenerateNewClaims(User user);
        string GetClaim(string token, string claimType);
        string CreateJsonToken(User user);
        bool ValidateJsonToken(string token);
    }
}