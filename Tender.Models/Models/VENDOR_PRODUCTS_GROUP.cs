using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class VENDOR_PRODUCTS_GROUP
    {
        public string VENDOR_ID { get; set; }
        public string PRODUCTS_GROUP_ID { get; set; }
        public string PRODUCTS_GROUP_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
    }
}
