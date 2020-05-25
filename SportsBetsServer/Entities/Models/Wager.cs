using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities.Models
{
    [Table("wager")]
    public class Wager
    {
        [Key]
        [Column("wager")]
        public Guid Id { get; set; }
        [Column("bet")]
        public ICollection<Bet> Bet { get; set; }
        [Required(ErrorMessage="Date created is required.")]
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage="Status is required.")]
        [Column("status")]
        public Status Status { get; set; }
        [Required(ErrorMessage="Win condition is required.")]
        [Column("win_condition")]
        public string WinCondition { get; set; }
        [Column("result")]
        public string Result { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
    }
    public enum Status
    {
        open,
        pending,
        closed
    }
}