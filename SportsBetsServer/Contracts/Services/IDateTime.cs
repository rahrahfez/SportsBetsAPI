using System;

namespace Contracts.Services 
{
  public interface IDateTime
  {
    DateTime Now { get; }
  }
}