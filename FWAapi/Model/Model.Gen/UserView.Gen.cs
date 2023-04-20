using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FWAapi.Model
{
	public partial class UserView : BaseModel
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

		private int? m_uAddressId;
		public int? uAddressId
		{
			get { return m_uAddressId; }
			set { SetDbField(ref m_uAddressId, value, nameof(this.uAddressId)); }
		}

		private int? m_aAddressId;
		public int? aAddressId
		{
			get { return m_aAddressId; }
			set { SetDbField(ref m_aAddressId, value, nameof(this.aAddressId)); }
		}

		private string m_Street = "";
		public string Street
		{
			get { return m_Street; }
			set { SetDbField(ref m_Street, value, nameof(this.Street)); }
		}

		private int? m_Number;
		public int? Number
		{
			get { return m_Number; }
			set { SetDbField(ref m_Number, value, nameof(this.Number)); }
		}

		private string m_City = "";
		public string City
		{
			get { return m_City; }
			set { SetDbField(ref m_City, value, nameof(this.City)); }
		}

		private string m_ZipCode = "";
		public string ZipCode
		{
			get { return m_ZipCode; }
			set { SetDbField(ref m_ZipCode, value, nameof(this.ZipCode)); }
		}
	}
}
