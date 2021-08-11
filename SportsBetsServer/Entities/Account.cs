using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities
{
    [Table("accounts")]
    public class Account 
    {
        [Key]
        [Column("id")]
        public Guid Id { get; private set; }
        [Column("username")]
        public string Username { get; private set; }
        [Column("password_hash")]
        public string HashedPassword { get; private set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; private set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [Column("last_login")]
        public DateTime LastLoginAt { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; } 
        [Column("refresh_token")]
        public string RefreshToken { get; set; }
        [Column("wagers")]
        public ICollection<Wager> Wagers { get; private set; }
        private Account() { }
        public Account(
            Guid id,
            string username,
            string hashedPassword,
            int availableBalance,
            DateTime createdAt,
            DateTime updatedAt,
            DateTime lastLogin)
        {
            Id = id;
            Username = username;
            HashedPassword = hashedPassword;
            AvailableBalance = availableBalance;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            LastLoginAt = lastLogin;

            Wagers = new HashSet<Wager>();
        }
    }
}

