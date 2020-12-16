using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Entities.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public int AvailableBalance { get; set; }
        public Role Role { get; set; }
    }
}
