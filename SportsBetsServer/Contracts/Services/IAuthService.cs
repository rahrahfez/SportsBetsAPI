using System.Threading.Tasks;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Services 
{
  public interface IAuthService 
  {
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    Task CreateCredentialsAsync(User user, string password);
    Task<User> LoginUserAsync(string username, string password);
  }
}