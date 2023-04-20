using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;

namespace FWAapi.Business
{
	public class Scope : IScope
	{
		public DbContext Db { get; }

		public Scope(DbContext dbContext)
		{
			this.Db = dbContext;
		}

		private bool m_DisposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!m_DisposedValue)
			{
				if (disposing)
					this.Db.Dispose();
			}
			m_DisposedValue = true;
		}

		public void Dispose()
		{
			Dispose(true);
		}
	}
}

