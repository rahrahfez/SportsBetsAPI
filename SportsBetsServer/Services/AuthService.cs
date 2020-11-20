using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Contracts.Repository;
using SportsBetsServer.Entities.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SportsBetsServer.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryWrapper _repo;

        public AuthService(IRepositoryWrapper repo) 
        {
            _repo = repo;
        }
        public void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
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

            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                user = null;
            }
            return user;
        }
    }
}