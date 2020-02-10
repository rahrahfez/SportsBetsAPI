using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities.Models
{
    [Table("bet")]
    public class Bet
    {
        [Key]
        [Column("bet")]
        public int Id { get; set; }
        [Column("wager")]
        [Required]
        public Wager Wager { get; set; }
        [Column("user")]
        [Required]
        public User User { get; set; }
    }
}