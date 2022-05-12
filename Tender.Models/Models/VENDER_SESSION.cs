using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class VENDER_SESSION
    {
        public string VENDOR_EMAIL { get; set; }
        public string VENDOR_ID { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string COUNTRY_NAME { get; set; }
        public int SUPPLIER { get; set; }
        public int PURCHASER { get; set; }
        public string VENDOR_USER_ID { get; set; }
    }
}
