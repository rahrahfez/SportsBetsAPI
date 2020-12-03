using SportsBetsServer.Entities.Models;
using Microsoft.Extensions.Configuration;

namespace SportsBetsServer.Contracts.Services 
{
  public interface IAuthService 
  {
    string CreatePasswordHash(string password);
    bool VerifyPassword(string password, string hashedPassword);
    string CreateJsonToken(IConfiguration config, User user);
  }
}