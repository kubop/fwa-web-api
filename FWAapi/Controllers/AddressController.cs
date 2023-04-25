using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FWAapi.Controllers.UserController;

namespace FWAapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public IList<Address> Get(string? orderBy)
        {
            IList<Address> u = _addressService.GetAllAddressesWithCount(orderBy);
            return u;
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var address = _addressService.GetObjectById(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            var oldAddress = _addressService.GetObjectById(id);
            if (oldAddress == null)
            {
                return NotFound();
            }

            oldAddress.Street = address.Street;
            oldAddress.Number = address.Number;
            oldAddress.ZipCode = address.ZipCode;
            oldAddress.City = address.City;

            _addressService.Update(oldAddress);
                
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Address addressToDelete = _addressService.GetObjectById(id);
            if (addressToDelete == null)
            {
                return NotFound();
            }

            _addressService.SoftDelete(addressToDelete); // TODO: Real soft delete

            return Ok();
        }
    }
}
