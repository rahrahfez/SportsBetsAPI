using System;
using System.Security.Claims;
using SportsBetsServer.Models.Account;

namespace SportsBetsServer.Contracts.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(AccountResponseDTO user);
        string CreateRefreshToken();
        ClaimsPrincipal GetClaimPrincipalFromToken(string token);
    }
}
