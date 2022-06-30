using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class VENDOR_COMPANY : COMMON
    {
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public string VENDOR_ID { get; set; }
        public string FLAG { get; set; }
    }
}
