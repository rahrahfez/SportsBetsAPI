using System.ComponentModel.DataAnnotations;

namespace SportsBetsServer.Models.Account
{
    public class UserCredentials
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
