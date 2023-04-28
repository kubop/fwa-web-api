using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FWAapi.Model
{
	[DebuggerDisplay("UserId: {UserId}")]
	public partial class User : BaseModel
	{
		private int m_UserId;
		public int UserId
		{
			get { return m_UserId; }
			set { SetDbField(ref m_UserId, value, nameof(this.UserId)); }
		}

		private string m_FirstName = "";
		public string FirstName
		{
			get { return m_FirstName; }
			set { SetDbField(ref m_FirstName, value, nameof(this.FirstName)); }
		}

		private string m_LastName = "";
		public string LastName
		{
			get { return m_LastName; }
			set { SetDbField(ref m_LastName, value, nameof(this.LastName)); }
		}

		private string m_Login = "";
		public string Login
		{
			get { return m_Login; }
			set { SetDbField(ref m_Login, value, nameof(this.Login)); }
		}

		private string m_Password = "";
		public string Password
		{
			get { return m_Password; }
			set { SetDbField(ref m_Password, value, nameof(this.Password)); }
		}

		private int? m_AddressId;
		public int? AddressId
		{
			get { return m_AddressId; }
			set { SetDbField(ref m_AddressId, value, nameof(this.AddressId)); }
		}

		private DateTime m_Modified;
		public DateTime Modified
		{
			get { return m_Modified; }
			set { SetDbField(ref m_Modified, value, nameof(this.Modified)); }
		}
	}
}
