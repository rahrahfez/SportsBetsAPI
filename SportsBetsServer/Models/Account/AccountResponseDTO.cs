using System;

namespace SportsBetsServer.Models.Account
{
    public class AccountResponseDTO
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public DateTime LastLoginAt { get; set; }
        public int AvailableBalance { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        private AccountResponseDTO() { }
        public AccountResponseDTO(Guid id, string username, int available_balance)
        {
            Id = id;
            Username = username;
            AvailableBalance = available_balance;
        }
    }
}

