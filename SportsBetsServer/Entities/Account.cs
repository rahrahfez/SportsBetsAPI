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
        public Guid Id { get; set; }
        [Column("username")]
        public string Username { get; set; }
        [Column("password_hash")]
        public string HashedPassword { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; } 
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        //[Column("role")]
        //public Role Role { get; set; } 
        //[Column("refresh_token")]
        //public IList<RefreshToken> RefreshToken { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }
}

