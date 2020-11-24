using Microsoft.EntityFrameworkCore;
using SportsBetsServer.Entities;
using Xunit;
using System;

namespace SportsBetsAPI.Tests.Controllers
{
    public class UserControllerTest
    {
        private DbContextOptions<RepositoryContext> _options;
        private readonly RepositoryContext _context;
        public UserControllerTest()
        {
            _context = new RepositoryContext(_options);
        }
    }
}