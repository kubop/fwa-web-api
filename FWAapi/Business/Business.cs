using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FWAapi.DbAccess;

namespace FWAapi.Business
{
	public partial class Business
	{
        private DbContextFactory m_DbContextFactory;

        public void SetDbContextFactory(/*Func<DbContext>*/DbContextFactory factory)
        {
            m_DbContextFactory = factory;
        }

        public IScope CreateScope()
        {
            return new Scope(CreateDbContext());
        }

        protected DbContext CreateDbContext()
        {
            return m_DbContextFactory.CreateInstance();
        }

        protected virtual void OnExecuteError(IScope scope, Exception ex)
        {
        }
    }
}

