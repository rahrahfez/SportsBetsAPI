using System;

namespace SportsBetsServer.Contracts.Services 
{
  public interface IDateTime
  {
    DateTime Now { get; }
  }
}