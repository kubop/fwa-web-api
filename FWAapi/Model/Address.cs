using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace FWAapi.Model
{
	public partial class Address
	{
        [NotMapped]
        public int CountUsers { get; set; } = 0;
    }
}
