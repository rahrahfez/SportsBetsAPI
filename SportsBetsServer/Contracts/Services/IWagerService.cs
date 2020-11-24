using System;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateWager(WagerToCreate wager);
    }
}