using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_BIDDING
    {
        public string RFQ_NUMBER { get; set; }
        public string QUOTE_NUMBER { get; set; }
        public int RFQ_SL { get; set; }
        public string VENDOR_ID { get; set; }

        [NotMapped]
        public string LOCAL_IMPORT { get; set; }
        [NotMapped]
        public bool RE_BID { get; set; }
        [NotMapped]
        public bool LOWER_RATE { get; set; }
        [NotMapped]
        public DateTime START_DATE { get; set; }
        [NotMapped]
        public DateTime END_DATE { get; set; }
        [NotMapped]
        public DateTime LAST_DELIVERY_DATE { get; set; }


        public DateTime SUBMIT_DATE { get; set; }

        public string PRODUCTS_ID { get; set; }
        public string PRODUCTS_DESC { get; set; }
        [NotMapped]
        public string TENDER_PRODUCTS_DESC { get; set; }
        [NotMapped]
        public decimal QUOTATION_PRODUCTS_RATE { get; set; }
        public decimal PRODUCTS_RATE { get; set; }
        public int PRODUCTS_QUANTITY { get; set; }


        [NotMapped]
        public bool PARTIAL_SHIPMENT { get; set; }
        [NotMapped]
        public int TENDER_SHIPMENT_MODE { get; set; }
        [NotMapped]
        public int TENDER_PORT_ID { get; set; }

        public int SHIPMENT_MODE { get; set; }
        public int PORT_ID { get; set; }

        [NotMapped]
        public string DELIVERY_ADDRESS { get; set; }
        [NotMapped]
        public string RECEIVER_NAME { get; set; }
        [NotMapped]
        public string RECEIVER_DETAILS { get; set; }

        public string LOADING_ADDRESS { get; set; }
        public string SENDER_NAME { get; set; }
        public string SENDER_DETAILS { get; set; }

        public RFQ_BIDDING getAll()
        {
            RFQ_BIDDING obj = new RFQ_BIDDING();
            obj.RFQ_NUMBER = "RFQ01255255555";
            obj.RFQ_SL = 123;
            obj.VENDOR_ID = "AK traders";
            obj.LOCAL_IMPORT = "example@ex.com";
            //obj.RE_BID = "Merul Badda, Dit project";
            //obj.LOWER_RATE = "Merul Badda, Dit project";
            //obj.START_DATE = true;
            //obj.END_DATE = true;
            //obj.VENDOR_CATEGORY = new VENDOR_CATEGORY().getAll();
            //obj.VENDOR_CERTIFICATE = new VENDOR_CERTIFICATE().getAll();
            //obj.VENDOR_DOCUMENTS = new VENDOR_DOCUMENTS().getAll();
            //obj.VENDOR_PRODUCTS = new VENDOR_PRODUCTS().getAll();
            return obj;
        }
    }
}
