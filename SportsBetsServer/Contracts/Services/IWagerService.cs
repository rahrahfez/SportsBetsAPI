using System;
using Entities.Models;

namespace Contracts.Services
{
    public interface IWagerService
    {
        Wager CreateNewWager(Guid userId);
    }
}