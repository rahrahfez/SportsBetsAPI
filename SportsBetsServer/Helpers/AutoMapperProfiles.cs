using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SportsBetsServer.Models.Account;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Account, AccountResponseDTO>();
        }           
    }
}
