using System;

namespace SportsBetsServer.Entities.Extensions
{
    public class WagerToCreate
    {
        public Guid UserId { get; set; }
        public string WinCondition { get; set; }
        public int Amount { get; set; }
    }
}