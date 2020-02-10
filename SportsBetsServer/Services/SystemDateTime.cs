using System;
using Contracts.Services;

namespace Services
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