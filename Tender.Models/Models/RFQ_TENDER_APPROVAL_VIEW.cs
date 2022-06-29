using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_TENDER_APPROVAL_VIEW : RFQ_BIDDING
    {
        public string APPROVAL_ID { get; set; }
        public string RFQ_NUMBER { get; set; }
        public string QUOTE_NUMBER { get; set; }
        public string VENDOR_ID { get; set; }
        public DateTime APPROVAL_DATE { get; set; }
        public string APPROVAL_NOTE { get; set; }
		public string FIRST_APRV_STATUS { get; set; }
		public DateTime FIRST_APRV_DATE { get; set; }
		public string FIRST_APRV_NOTE { get; set; }
		public string FIRST_APRV_BY { get; set; }
		public string SEC_APRV_STATUS { get; set; }
		public DateTime SEC_APRV_DATE { get; set; }
		public string SEC_APRV_NOTE { get; set; }
		public string SEC_APRV_BY { get; set; }
		public string THIRD_APRV_STATUS { get; set; }
		public DateTime THIRD_APRV_DATE { get; set; }
		public string THIRD_APRV_NOTE { get; set; }
		public string THIRD_APRV_BY { get; set; }
	}
}
