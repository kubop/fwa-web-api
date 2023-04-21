using FWAapi.Business;
using FWAapi.Model;

namespace FWAapi.Services;

public class AddressService
{
    private readonly IBusinessProvider _business;

    public AddressService(IBusinessProvider business)
    {
        _business = business;
    }

    public Address GetObjectById(int addressId, IGenericScope scope = null)
    {
        return _business.Get<AddressBusiness>().GetObject(addressId, scope as IScope);
    }

    public IList<Address> GetAllAddresses(IGenericScope scope = null)
    {
        return _business.Get<AddressBusiness>().GetAllAddresses();
    }

    public IList<Address> GetAllAddressesWithCount(IGenericScope scope = null)
    {
        return _business.Get<AddressBusiness>().GetAllAddressesWithCount();
    }

    public void SoftDelete(Address address, IGenericScope scope = null)
    {
        _business.Get<AddressBusiness>().Delete(address);
    }

    public void Update(Address obj, IGenericScope scope = null)
    {
        _business.Get<AddressBusiness>().Update(obj);
    }
}
