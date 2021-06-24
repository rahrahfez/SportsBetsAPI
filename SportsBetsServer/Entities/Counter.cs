using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsBetsServer.Entities
{
    [Table("counter")]
    public class Counter
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("count")]
        public int Count { get; set; }
    }
}
