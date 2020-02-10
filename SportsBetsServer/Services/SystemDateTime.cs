using System;
using SportsBetsServer.Contracts.Services;

namespace SportsBetsServer.Services
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now 
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}