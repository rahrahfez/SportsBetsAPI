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
        [Column("user")]
        public User User { get; set; }
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage="Status is required.")]
        [Column("status")]
        public Status Status { get; set; }
        [Required(ErrorMessage="Win condition is required.")]
        [Column("result")]
        public Result Result { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
    }
    public enum Status
    {
        open,
        pending,
        closed
    }
    public enum Result
    {
        win,
        loss,
        push
    }
}