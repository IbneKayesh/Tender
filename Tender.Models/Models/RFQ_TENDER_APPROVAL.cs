using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_TENDER_APPROVAL
    {
        public string APPROVAL_ID { get; set; }
        public string RFQ_NUMBER { get; set; }
        public string QUOTE_NUMBER { get; set; }
        public string VENDOR_ID { get; set; }
        public DateTime APPROVAL_DATE { get; set; }
        public string APPROVAL_NOTE { get; set; }
    }
}
