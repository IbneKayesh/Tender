using System;
using System.Collections.Generic;

namespace Tender.Models.Models
{
    public class VENDOR_DETAILS: VENDOR
    {

        public string CONTACT_NAME { get; set; }
        public string CONTACT_NUMBER { get; set; }
        
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }

        public int YEAR_OF_ESTABLISHMENT { get; set; }
        public Int64 YEARLY_TRUNOVER { get; set; }
        public string NO_OF_EMPLOYEES { get; set; }
        public string TAX_NUMBER { get; set; }
        public string TRADE_NUMBER { get; set; }

        public bool IS_SUBMIT { get; set; }
        public bool IS_APPROVE { get; set; }


        public virtual List<VENDOR_CATEGORY> VENDOR_CATEGORY { get; set; }
        public virtual List<VENDOR_CERTIFICATE> VENDOR_CERTIFICATE { get; set; }
        public virtual List<VENDOR_DOCUMENTS> VENDOR_DOCUMENTS { get; set; }
        public virtual List<VENDOR_PRODUCTS> VENDOR_PRODUCTS { get; set; }

    }
}
