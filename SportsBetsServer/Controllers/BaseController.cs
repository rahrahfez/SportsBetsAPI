using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsBetsServer.Entities;

namespace SportsBetsServer.Controllers
{
    [Controller]
    public class BaseController : ControllerBase
    {
        public Account Account => (Account)HttpContext.Items["Account"];
    }
}
