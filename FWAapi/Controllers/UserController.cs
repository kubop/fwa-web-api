using FWAapi.Business;
using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Mvc;
namespace FWAapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IList<UserView> Get(string? orderBy)
    {
        IList<UserView> u = _userService.ListForGrid(orderBy);

        foreach(UserView user in u)
        {
            user.Password = string.Empty;
        }

        return u;
    }
}
