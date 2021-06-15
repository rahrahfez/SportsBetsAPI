using Microsoft.AspNetCore.Mvc;
using SportsBetsServer.Contracts.Services;
using SportsBetsServer.Entities;
using SportsBetsServer.Repository;
using SportsBetsServer.Models.Account;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using LoggerService;
using SportsBetsServer.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace SportsBetsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILoggerManager _logger;
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public AuthController(
            ILoggerManager logger, 
            IAccountService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }
    }
    
}