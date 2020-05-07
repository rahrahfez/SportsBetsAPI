using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities.Models 
{
    [Table("credential")]
    public class Credential
    {
        [Column("user")]
        [ForeignKey("Id")]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Column("password_hash")]
        public byte[] PasswordHash { get; set; }
        [Required]
        [Column("password_salt")]
        public byte[] PasswordSalt { get; set; }
        public User User { get; set; }
    }
}