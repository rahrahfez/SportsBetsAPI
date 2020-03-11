using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateWager(Wager wager);
        Wager AcceptWager(Wager wager);
    }
}