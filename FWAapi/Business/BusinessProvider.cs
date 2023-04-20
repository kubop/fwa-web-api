using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FWAapi.Business
{
	public class BusinessProvider : IBusinessProvider
	{
		private Func<Type, Business> m_Factory;

		public BusinessProvider(Func<Type, Business> factory)
		{
			m_Factory = factory;
		}

		public T Get<T>() where T : Business
		{
			return (T)m_Factory.Invoke(typeof(T));
		}
	}
}
