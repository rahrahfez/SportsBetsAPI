using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models 
{
    [Table("credential")]
    public class Credential
    {
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