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
        [Column("wager")]
        public ICollection<Wager> Wagers { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters.")]
        [Column("username")]
        public string Username { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; } 
        [Required(ErrorMessage="Date created is required.")]
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "User role is required.")]
        [Column("role")]
        public Role Role { get; set; } 
        [Required]
        [Column("password_hash")]
        public string HashedPassword { get; set; }
        [Required]
        [Column("verification_token")]
        public string VerificationToken { get; set; }
        public List<RefreshToken> RefeshTokens { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }
}

