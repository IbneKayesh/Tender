using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{

    public class VENDOR_PRODUCTS
    {
        public string VENDOR_ID { get; set; }
        public string PRODUCTS_ID { get; set; }
        public string PRODUCTS_NAME { get; set; }

        public bool IS_ACTIVE { get; set; }
        public List<VENDOR_PRODUCTS> getAll()
        {
            return new List<VENDOR_PRODUCTS> {
                new VENDOR_PRODUCTS { VENDOR_ID="ABC", PRODUCTS_ID = "10", PRODUCTS_NAME = "Potato" ,IS_ACTIVE=true},
                new VENDOR_PRODUCTS { VENDOR_ID="ABC", PRODUCTS_ID = "11", PRODUCTS_NAME = "Ata" ,IS_ACTIVE=false},
                new VENDOR_PRODUCTS { VENDOR_ID="ABC", PRODUCTS_ID = "12", PRODUCTS_NAME = "Moyda " ,IS_ACTIVE=true},
                new VENDOR_PRODUCTS { VENDOR_ID="ABC", PRODUCTS_ID = "10", PRODUCTS_NAME = "AC" ,IS_ACTIVE=false}
            };
        }
    }
}
