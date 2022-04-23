using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{

    public class VENDOR_DOCUMENTS
    {
        public string VENDOR_ID { get; set; }
        public string DOCUMENTS_ID { get; set; }
        public string DOCUMENTS_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }

        public List<VENDOR_DOCUMENTS> getAll()
        {
            return new List<VENDOR_DOCUMENTS> {
                new VENDOR_DOCUMENTS { VENDOR_ID="ABC", DOCUMENTS_ID = "10", DOCUMENTS_NAME = "Halal Certi" ,IS_ACTIVE=true},
                new VENDOR_DOCUMENTS { VENDOR_ID="ABC", DOCUMENTS_ID = "11", DOCUMENTS_NAME = "Importer" ,IS_ACTIVE=false},
                new VENDOR_DOCUMENTS { VENDOR_ID="ABC", DOCUMENTS_ID = "12", DOCUMENTS_NAME = "ISO " ,IS_ACTIVE=true},
                new VENDOR_DOCUMENTS { VENDOR_ID="ABC", DOCUMENTS_ID = "10", DOCUMENTS_NAME = "NISO" ,IS_ACTIVE=false}
            };
        }
    }

}
