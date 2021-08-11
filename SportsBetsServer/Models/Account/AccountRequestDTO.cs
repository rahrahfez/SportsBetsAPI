using System.ComponentModel.DataAnnotations;

namespace SportsBetsServer.Models.Account
{
    public class AccountRequestDTO
    {
        [Required]
        public string Username { get; private set; }
        [Required]
        public string Password { get; private set; }
        private AccountRequestDTO() { }
        public AccountRequestDTO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
