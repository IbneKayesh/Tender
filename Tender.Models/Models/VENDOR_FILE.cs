using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class VENDOR_FILE :COMMON
    {
		public string FILE_ID { get; set; }
		public string VENDOR_ID { get; set; }
		public string FILE_PATH { get; set; }
		public string FILE_NUMBER { get; set; }
		public string FILE_NAME { get; set; }
		public string FILE_TYPE { get; set; }
		public string DOCUMENT_TYPE { get; set; }
		public string DOC_ID { get; set; }		
        [NotMapped]
		public string DOC_NAME { get; set; }
	}
}
