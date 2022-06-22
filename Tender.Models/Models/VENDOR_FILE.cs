using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class VENDOR_FILE
    {
		public string FILE_ID { get; set; }
		public string VENDOR_ID { get; set; }
		public string FILE_PATH { get; set; }
		public string FILE_NUMBER { get; set; }
		public string FILE_NAME { get; set; }
		public string FILE_TYPE { get; set; }
		public string DOCUMENT_TYPE { get; set; }
		public string DOC_ID { get; set; }
		public decimal IS_ACTIVE { get; set; }
		public DateTime CREATE_DATE { get; set; }
		public string CREATE_USER { get; set; }
		public string CREATE_DEVICE { get; set; }
		public DateTime UPDATE_DATE { get; set; }
		public string UPDATE_USER { get; set; }
		public string UPDATE_DEVICE { get; set; }
		public decimal VER { get; set; }
	}
}
