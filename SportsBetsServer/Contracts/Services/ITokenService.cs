using System;
using System.Security.Claims;
using SportsBetsServer.Models.Account;

namespace SportsBetsServer.Contracts.Services
{
    public interface ITokenService
    {
        Claim[] GenerateNewUserClaim(AccountResponseDTO user);
        string CreateAccessToken(AccountResponseDTO user);
        string CreateRefreshToken();
    }
}
