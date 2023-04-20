using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FWAapi.Model
{
	[DebuggerDisplay("AddressId: {AddressId}")]
	public partial class Address : BaseModel
	{
		private int m_AddressId;
		public int AddressId
		{
			get { return m_AddressId; }
			set { SetDbField(ref m_AddressId, value, nameof(this.AddressId)); }
		}

		private string m_Street = "";
		public string Street
		{
			get { return m_Street; }
			set { SetDbField(ref m_Street, value, nameof(this.Street)); }
		}

		private int m_Number;
		public int Number
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
