using System.Threading.Tasks;
using Entities.Models;

namespace Contracts.Services 
{
  public interface IAuthService 
  {
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    Task<User> LoginUserAsync(string username, string password);
  }
}