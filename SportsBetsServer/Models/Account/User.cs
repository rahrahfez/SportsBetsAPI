using System;

namespace SportsBetsServer.Models.Account
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
    }
}

