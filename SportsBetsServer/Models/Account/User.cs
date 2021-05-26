using System;
using System.Collections.Generic;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Models.Account
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
        public Role Role { get; set; }
        public IList<RefreshToken> RefreshToken { get; set; }
    }
}

