using FWAapi.Business;
using FWAapi.Model;

namespace FWAapi.Services;

public class UserService
{
    private readonly IBusinessProvider _business;

    public UserService(IBusinessProvider business)
    {
        _business = business;
    }

    public User GetObjectById(int userId, IGenericScope scope = null)
    {
        return _business.Get<UserBusiness>().GetObject(userId, scope as IScope);
    }

    public IList<UserView> ListForGrid(string? orderBy, IGenericScope scope = null)
    {
        return _business.Get<UserViewBusiness>().ListForGrid(orderBy);
    }
}
