using Contracts.Services;
using Contracts.Repository;
using Entities.Models;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Services
{
    public class AuthService : IAuthService
    {
        private IRepositoryWrapper _repo;
        public AuthService(IRepositoryWrapper repo) 
        {
            _repo = repo;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public async Task<User> LoginUserAsync(string username, string password)
        {
            var user = await _repo.User.GetUserByUsernameAsync(username);
            var creds = await _repo.Auth.GetCredentialByUserId(user.Id);

            if(!VerifyPasswordHash(password, creds.PasswordHash, creds.PasswordSalt))
            {
                user = null;
            }

            return user;
        }
    }
}