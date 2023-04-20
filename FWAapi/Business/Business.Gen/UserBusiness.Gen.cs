using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;
using FWAapi.Model;

namespace FWAapi.Business
{
	public partial class UserBusiness : Business
	{
		public void Insert(User obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Insert(obj);
			}, scope);
		}

		public void Update(User obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Update(obj);
			}, scope);
		}

		public void Delete(User obj, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Delete(obj);
			}, scope);
		}

		public void Delete(int userId, IScope scope = null)
		{
			Execute((IScope s) =>
			{
				s.Db.Delete<User>().Where(x => x.UserId == userId).Execute();
			}, scope);
		}

		public User GetObject(int userId, IScope scope = null)
		{
			return Execute((IScope s) =>
			{
				return s.Db.From<User>().Where(x => x.UserId == userId).SelectAll().FirstOrDefault();
			}, scope);
		}
	}
}
