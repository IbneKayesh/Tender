using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class COMMON
    {
		public int IS_ACTIVE { get; set; }
		public DateTime CREATE_DATE { get; set; }
		public string CREATE_USER { get; set; }
		public string CREATE_DEVICE { get; set; }
		public DateTime UPDATE_DATE { get; set; }
		public string UPDATE_USER { get; set; }
		public string UPDATE_DEVICE { get; set; }
		public int VER { get; set; }
	}
}
