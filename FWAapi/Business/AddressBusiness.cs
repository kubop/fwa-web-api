using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public List<Address> GetAllAddressesWithCount(string? orderBy)
    {
        return Execute((IScope s) =>
        {
            return s.Db.From<Address>()
                .LeftJoin<User>((address, user) => address.AddressId == user.AddressId)
                .GroupBy(t => t.T1)
                // Default order
                .If(string.IsNullOrWhiteSpace(orderBy), exp => exp.OrderBy(u => u.AddressId))
                // Ascending param
                .If(orderBy == "Street", exp => exp.ThenBy(u => u.Street))
                .If(orderBy == "Number", exp => exp.ThenBy(u => u.Number))
                .If(orderBy == "ZipCode", exp => exp.ThenBy(u => u.ZipCode))
                .If(orderBy == "City", exp => exp.ThenBy(u => u.City))
                .If(orderBy == "CountUsers", exp => exp.ThenBy(t => Yamo.Sql.Aggregate.Count(t.T2.AddressId)))
                // Descending param
                .If(orderBy == "Street_DESC", exp => exp.ThenByDescending(u => u.Street))
                .If(orderBy == "Number_DESC", exp => exp.ThenByDescending(u => u.Number))
                .If(orderBy == "ZipCode_DESC", exp => exp.ThenByDescending(u => u.ZipCode))
                .If(orderBy == "City_DESC", exp => exp.ThenByDescending(u => u.City))
                .If(orderBy == "CountUsers_DESC", exp => exp.ThenByDescending(t => Yamo.Sql.Aggregate.Count(t.T2.AddressId)))
                // Get all
                .SelectAll()
                .ExcludeT2()
                .Include(t => t.T1.CountUsers, t => Yamo.Sql.Aggregate.Count(t.T2.AddressId))
                .ToList();

            // Possible another solution, can take a look if needed
            //return s.Db.From<Address>()
            //    .LeftJoin(u =>
            //    {
            //        return u.From<User>()
            //            .GroupBy(x => x.AddressId)
            //            .Select(x => new { AddressId = x.AddressId, CountUsers = Yamo.Sql.Aggregate.Count() });
            //    })
            //    .On(t => t.T1.AddressId == t.T2.AddressId)
            //    .SelectAll()
            //    .Include(t => t.T1.CountUsers, t => t.T2.CountUsers)
            //    .ToList();
        });
    }
}
