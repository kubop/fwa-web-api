using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;
using FWAapi.Model;

namespace FWAapi.Business
{
	public partial class AddressBusiness : Business
	{
		public void Insert(Address obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Insert(obj);
			}, scope);
		}

		public void Update(Address obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Update(obj);
			}, scope);
		}

		public void Delete(Address obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Delete(obj);
			}, scope);
		}

		public void Delete(int addressId, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Delete<Address>().Where(x => x.AddressId == addressId).Execute();
			}, scope);
		}

		public Address GetObject(int addressId, IScope scope = null)
		{
			return Execute((IScope s) =>
			{
				return s.Db.From<Address>().Where(x => x.AddressId == addressId).SelectAll().FirstOrDefault();
			}, scope);
		}
	}
}
