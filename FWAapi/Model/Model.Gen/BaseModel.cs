using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Common.Objects;
using Yamo;

namespace FWAapi.Model
{
	public class BaseModel : NotifyPropertyChanged, IHasDbPropertyModifiedTracking
	{
		private readonly HashSet<string> m_ModifiedDbProperties = new HashSet<string>();

		public void ResetDbPropertyModifiedTracking()
		{
			m_ModifiedDbProperties.Clear();
		}

		public bool IsAnyDbPropertyModified()
		{
			return m_ModifiedDbProperties.Any();
		}

		public bool IsDbPropertyModified(string propertyName)
		{
			return m_ModifiedDbProperties.Contains(propertyName);
		}

		protected virtual bool SetDbField<T>(ref T field, T value, string propertyName)
		{
			if (SetField<T>(ref field, value, propertyName))
			{
				m_ModifiedDbProperties.Add(propertyName);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
