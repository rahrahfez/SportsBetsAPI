using System;

namespace SportsBetsServer.Entities.Models.Extensions
{
    public class WagerToCreate
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}