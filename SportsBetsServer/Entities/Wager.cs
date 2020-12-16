using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities
{
    [Table("wager")]
    public class Wager
    {
        [Key]
        [Column("wager")]
        public Guid Id { get; set; }
        [ForeignKey("UserId")]
        [Column("user")]
        public Guid UserId { get; set; }
        [Column("date_created")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage="Status is required.")]
        [Column("status")]
        public Status Status { get; set; }
        [Column("result")]
        public Result? Result { get; set; }
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