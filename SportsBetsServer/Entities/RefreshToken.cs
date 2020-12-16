using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SportsBetsServer.Entities
{
    [Owned]
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public Account Account { get; set; }
    }
}
