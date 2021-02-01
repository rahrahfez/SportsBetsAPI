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
        [Column("account")]
        public Guid Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; } 
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Column("role")]
        public Role Role { get; set; } 
        [Column("password_hash")]
        public string HashedPassword { get; set; }
        [Column("refresh_token")]
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }
}

