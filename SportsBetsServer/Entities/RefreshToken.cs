﻿using System;
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
        public Account Account { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Token { get; set; }
    }
}
