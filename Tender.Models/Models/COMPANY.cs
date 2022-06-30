using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class COMPANY :COMMON
    {
		public string COMPANY_ID { get; set; }
		public string COMPANY_NAME { get; set; }
		public string COMPANY_ADDRESS { get; set; }
		public string COMPANY_LOGO { get; set; }
		public string COMPANY_CONTACT { get; set; }
		public string COMPANY_MAIL { get; set; }
	}
}
