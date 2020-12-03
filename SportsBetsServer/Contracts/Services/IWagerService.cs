using System;
using SportsBetsServer.Entities.Models;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateWager(WagerToCreate wager);
    }
}