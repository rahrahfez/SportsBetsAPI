using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities.Models
{
    [Table("user")]
    public class User 
    {
        [Key]
        [Column("user")]
        public Guid Id { get; set; }
        [Column("bet")]
        public ICollection<Bet> Bet { get; set; }
        [Required(ErrorMessage="Username is required.")]
        [StringLength(30, ErrorMessage="Username cannot be longer than 30 characters.")]
        [Column("username")]
        public string Username { get; set; }
        [Column("available_balance")]
        public int AvailableBalance { get; set; } 
        [Required(ErrorMessage="Date created is required.")]
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage ="User role is required.")]
        [Column("user_role")]
        public string UserRole { get; set; }
        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }
        [Required]
        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }
    }
}