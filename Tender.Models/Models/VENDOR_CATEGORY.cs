using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{

    public class VENDOR_CATEGORY
    {
        public string VENDOR_ID { get; set; }
        public string CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public List<VENDOR_CATEGORY> getAll()
        {
            return new List<VENDOR_CATEGORY> {
                new VENDOR_CATEGORY { VENDOR_ID="ABC", CATEGORY_ID = "10", CATEGORY_NAME = "Manufacturer" ,IS_ACTIVE=true},
                new VENDOR_CATEGORY { VENDOR_ID="ABC", CATEGORY_ID = "11", CATEGORY_NAME = "Importer" ,IS_ACTIVE=false},
                new VENDOR_CATEGORY { VENDOR_ID="ABC", CATEGORY_ID = "12", CATEGORY_NAME = "Distributor" ,IS_ACTIVE=true},
                new VENDOR_CATEGORY { VENDOR_ID="ABC", CATEGORY_ID = "13", CATEGORY_NAME = "Retailer" ,IS_ACTIVE=false}
            };
        }
    }
}
