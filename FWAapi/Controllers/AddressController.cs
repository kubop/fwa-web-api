using FWAapi.Model;
using FWAapi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
