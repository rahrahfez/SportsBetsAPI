using System;
using SportsBetsServer.Entities;
using SportsBetsServer.Entities.Models.Extensions;

namespace SportsBetsServer.Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateWager(WagerToCreate wager);
    }
}