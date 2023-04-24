using FWAapi.Business;
using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace FWAapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AddressService _addressService;

    public UserController(UserService userService, AddressService addressService)
    {
        _userService = userService;
        _addressService = addressService;
    }

    [HttpGet]
    public IList<UserView> Get(string? orderBy)
    {
        IList<UserView> u = _userService.ListForGrid(orderBy);

        foreach (UserView user in u)
        {
            user.Password = string.Empty;
        }

        return u;
    }

    public class UserAddressesModel
    {
        public User User { get; set; }
        public IList<Address> Addresses { get; set; } = new List<Address>();
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
        var user = _userService.GetObjectById(id);
        if (user == null)
        {
            return NotFound();
        }
        user.Password = "";

        var addressList = _addressService.GetAllAddresses();

        var model = new UserAddressesModel
        {
            User = user,
            Addresses = addressList
        };

        return Ok(model);
    }

    [HttpPut("{id}")]
    public IActionResult Edit(int id, [FromBody] User user)
    {
        if (id != user.UserId)
        {
            return NotFound();
        }

        var oldUser = _userService.GetObjectById(id);
        if (oldUser == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(user.NewPassword))
        {
            user.Password = user.NewPassword;
        }
        else
        {
            user.Password = oldUser.Password;
        }

        ModelState.ClearValidationState(nameof(user));
        TryValidateModel(user, nameof(user));

        if (ModelState.IsValid)
        {
            _userService.Update(user);
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public  IActionResult Delete(int id)
    {
        User userToDelete = _userService.GetObjectById(id);
        if (userToDelete == null)
        {
            return NotFound();
        }

        _userService.SoftDelete(userToDelete); // TODO: Real soft delete

        return Ok();
    }
}
