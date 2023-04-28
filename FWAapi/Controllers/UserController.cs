using Azure;
using FWAapi.Business;
using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
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
            return BadRequest();
        }

        var oldUser = _userService.GetObjectById(id);
        if (oldUser == null)
        {
            return NotFound();
        }

        // Check if the record was modified by someone else in the meantime
        if (!oldUser.VerCol.SequenceEqual(user.VerCol))
        {
            return Conflict();
        }

        oldUser.FirstName = user.FirstName;
        oldUser.LastName = user.LastName;
        oldUser.Login = user.Login;
        oldUser.AddressId = user.AddressId;
        if (!string.IsNullOrEmpty(user.NewPassword))
        {
            oldUser.Password = user.NewPassword;
        }

        var validationResult = Validate(oldUser);
        if (validationResult == "success")
        {
            _userService.Update(oldUser);
            return Ok(new { newVerCol = oldUser.VerCol });
        } else
        {
            return BadRequest(validationResult);
        }
    }

    [HttpPatch("{id}")]
    public IActionResult Patch(int id, [FromBody] JsonPatchDocument<User> user)
    {
        //if (id != user)
        //{
        //    return BadRequest();
        //}

        var oldUser = _userService.GetObjectById(id);
        if (oldUser == null)
        {
            return NotFound();
        }

        user.ApplyTo(oldUser);

        //oldUser.FirstName = user.FirstName;
        //oldUser.LastName = user.LastName;
        //oldUser.Login = user.Login;
        //oldUser.AddressId = user.AddressId;
        //if (!string.IsNullOrEmpty(user.NewPassword))
        //{
        //    oldUser.Password = user.NewPassword;
        //}

        var validationResult = Validate(oldUser);
        if (validationResult == "success")
        {
            _userService.Update(oldUser);
            return Ok();
        }
        else
        {
            return BadRequest(validationResult);
        }
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

    private static string Validate(User user)
    {
        if (user == null) return "Error: User not found";

        if (string.IsNullOrEmpty(user.FirstName)) return "Error: FirstName attribute is required";
        if (string.IsNullOrEmpty(user.LastName)) return "Error: LastName attribute is required";
        if (string.IsNullOrEmpty(user.Login)) return "Error: Login attribute is required";
        if (string.IsNullOrEmpty(user.Password)) return "Error: Password attribute is required";

        if (user.Login.Length > 10) return "Error: Login attribute can't exceed 10 characters";

        return "success";
    }
}
