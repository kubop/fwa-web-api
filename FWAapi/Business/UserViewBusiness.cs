using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;
using FWAapi.Model;

namespace FWAapi.Business
{
	public partial class UserViewBusiness
	{
        public IList<UserView> ListForGrid(string? orderBy, IScope scope = null)
        {
            return Execute((IScope s) =>
            {
                return s.Db.From<UserView>()
                    // Default
                    .If(string.IsNullOrWhiteSpace(orderBy), exp => exp.OrderBy(u => u.UserId))
                    // Ascending param
                    .If(orderBy == "FirstName", exp => exp.ThenBy(u => u.FirstName))
                    .If(orderBy == "LastName", exp => exp.ThenBy(u => u.LastName))
                    .If(orderBy == "Login", exp => exp.ThenBy(u => u.Login))
                    // Descending param
                    .If(orderBy == "FirstName_DESC", exp => exp.ThenByDescending(u => u.FirstName))
                    .If(orderBy == "LastName_DESC", exp => exp.ThenByDescending(u => u.LastName))
                    .If(orderBy == "Login_DESC", exp => exp.ThenByDescending(u => u.Login))
                    // Get all
                    .SelectAll().ToList();
            });
        }
    }
}
