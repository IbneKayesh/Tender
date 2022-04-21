using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_MASTER
    {
        public string RFQ_NUMBER { get; set; }
        public string VENDOR_ID { get; set; }
        public bool SELL_BUY { get; set; }
        public string LOCAL_IMPORT { get; set; }
        public bool RE_BID { get; set; }
        public bool LOWER_RATE { get; set; }

        public DateTime START_DATE { get; set; }
        public DateTime END_DATE { get; set; }


        public string PRODUCTS_ID { get; set; }
        public string PRODUCTS_DESC { get; set; }
        public decimal PRODUCTS_RATE { get; set; }
        public int PRODUCTS_QUANTITY { get; set; }

        
        public bool PARTIAL_SHIPMENT { get; set; }
        public int SHIPMENT_MODE { get; set; }
        public int PORT_ID { get; set; }
        public string DELIVERY_ADDRESS { get; set; }
        public string RECEIVER_NAME { get; set; }
        public string RECEIVER_DETAILS { get; set; }
        
    }
}
