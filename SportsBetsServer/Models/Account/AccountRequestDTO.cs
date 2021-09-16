using System.ComponentModel.DataAnnotations;

namespace SportsBetsServer.Models.Account
{
    public class AccountRequestDTO
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
