using System;
using SportsBetsServer.Entities.Models;

namespace SportsBetsServer.Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateNewWager(Guid userId);
    }
}