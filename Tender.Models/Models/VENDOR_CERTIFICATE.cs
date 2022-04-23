using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{

    public class VENDOR_CERTIFICATE
    {
        public string VENDOR_ID { get; set; }
        public string CERTIFICATE_ID { get; set; }
        public string CERTIFICATE_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }

        public List<VENDOR_CERTIFICATE> getAll()
        {
            return new List<VENDOR_CERTIFICATE> {
                new VENDOR_CERTIFICATE { VENDOR_ID="ABC", CERTIFICATE_ID = "10", CERTIFICATE_NAME = "ETIN" ,IS_ACTIVE=true},
                new VENDOR_CERTIFICATE { VENDOR_ID="ABC", CERTIFICATE_ID = "11", CERTIFICATE_NAME = "Importer" ,IS_ACTIVE=false},
                new VENDOR_CERTIFICATE { VENDOR_ID="ABC", CERTIFICATE_ID = "12", CERTIFICATE_NAME = "ISO " ,IS_ACTIVE=true},
                new VENDOR_CERTIFICATE { VENDOR_ID="ABC", CERTIFICATE_ID = "10", CERTIFICATE_NAME = "NISO" ,IS_ACTIVE=false}
            };
        }
    }
}
