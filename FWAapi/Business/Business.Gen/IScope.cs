using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;

namespace FWAapi.Business
{
	public interface IScope : IDisposable, IGenericScope
	{
		DbContext Db { get; }
	}

    public interface IGenericScope { }
}

