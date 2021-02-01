using System;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Models.Account
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public bool IsVerified { get; set; }
    }
}

