using FWAapi.Business;
using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FWAapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IList<UserView> Get(string? orderBy)
        {
            IList<UserView> u = _userService.ListForGrid(orderBy);
            return u;
        }
    }
}
