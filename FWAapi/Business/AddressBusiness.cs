using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;
using FWAapi.Model;

namespace FWAapi.Business;

public partial class AddressBusiness
{
    public List<Address> GetAllAddresses()
    {
        return Execute((IScope s) =>
        {
            return s.Db.From<Address>().SelectAll().ToList();
        });
    }

    public List<Address> GetAllAddressesWithCount()
    {
        return Execute((IScope s) =>
        {
            return s.Db.From<Address>()
                .LeftJoin<User>((address, user) => address.AddressId == user.AddressId)
                .GroupBy(t => t.T1)
                .SelectAll()
                .ExcludeT2()
                .Include(t => t.T1.CountUsers, t => Yamo.Sql.Aggregate.Count(t.T2.AddressId))
                .ToList();
        });
    }
}
