using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities.Models 
{
    [Table("credential")]
    public class Credential
    {
        [Required]
        [ForeignKey("Id")]
        [Column("user")]
        public User User { get; set; }
        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }
        [Required]
        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }
    }
}