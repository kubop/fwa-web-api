using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FWAapi.Business
{
	public interface IBusinessProvider
	{
		T Get<T>() where T : Business;
	}
}
