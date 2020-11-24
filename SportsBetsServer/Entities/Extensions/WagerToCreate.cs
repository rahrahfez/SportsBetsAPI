using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Entities.Extensions
{
    public class WagerToCreate
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}