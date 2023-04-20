using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit.Common.Utils;

namespace FWAapi.Business
{
	public abstract partial class Business
	{
		public void Execute(Action<IScope> action, IScope scope = null)
		{
			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						action(sc);
						sc.Db.Database.CommitTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				action(scope);
			}
		}

		public async Task Execute(Func<IScope, Task> action, IScope scope = null)
		{
			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						await action(sc);
						sc.Db.Database.CommitTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				await action(scope);
			}
		}

		public T Execute<T>(Func<IScope, T> func, IScope scope = null)
		{
			T result;

			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						result = func(sc);
						sc.Db.Database.CommitTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				result = func(scope);
			}

			return result;
		}

		public async Task<T> Execute<T>(Func<IScope, Task<T>> func, IScope scope = null)
		{
			T result;

			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						result = await func(sc);
						sc.Db.Database.CommitTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				result = await func(scope);
			}

			return result;
		}

		public T ExecuteOrRollback<T>(Func<IScope, T> func, IScope scope = null) where T : Result
		{
			T result;

			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						result = func(sc);

						if (result.IsSuccess)
							sc.Db.Database.CommitTransaction();
						else
							sc.Db.Database.RollbackTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				result = func(scope);
			}

			return result;
		}

		public async Task<T> ExecuteOrRollback<T>(Func<IScope, Task<T>> func, IScope scope = null) where T : Result
		{
			T result;

			if (scope == null)
			{
				using (var sc = CreateScope())
				{
					try
					{
						sc.Db.Database.BeginTransaction();
						result = await func(sc);

						if (result.IsSuccess)
							sc.Db.Database.CommitTransaction();
						else
							sc.Db.Database.RollbackTransaction();
					}
					catch (Exception ex)
					{
						OnExecuteError(sc, ex);

						if (sc.Db.Database.Transaction != null)
						{
							sc.Db.Database.RollbackTransaction();
						}

						throw;
					}
				}
			}
			else
			{
				result = await func(scope);
			}

			return result;
		}
	}
}
