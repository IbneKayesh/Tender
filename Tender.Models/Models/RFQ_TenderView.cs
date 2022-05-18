using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_TenderView
    {
        public string RFQ_NUMBER { get; set; }

        public string VENDOR_ID { get; set; }

        public string SELL_BUY { get; set; }

        public string LOCAL_IMPORT { get; set; }

        public string RE_BID { get; set; }

        public string LOWER_RATE { get; set; }

        [DataType(DataType.Date)]
        public DateTime START_DATE { get; set; }
        [DataType(DataType.Date)]
        public DateTime END_DATE { get; set; }

        
        public string PRODUCTS_ID { get; set; }
        
        public string PRODUCTS_DESC { get; set; }
        
        public decimal PRODUCTS_RATE { get; set; }
        
        public decimal PRODUCTS_QUANTITY { get; set; }

        [DataType(DataType.Date)]
        public DateTime LAST_DELIVERY_DATE { get; set; }
        
        public string PARTIAL_SHIPMENT { get; set; }
        
        public string PORT_NAME { get; set; }
        
        public string DELIVERY_ADDRESS { get; set; }
        
        public string RECEIVER_NAME { get; set; }
        
        public string RECEIVER_DETAILS { get; set; }

        
        public string COST_EX_INC { get; set; }
        public string INCO_TERMS { get; set; }
        
        public string CURRENCY_NAME { get; set; }
        public decimal CURRENCY_RATE { get; set; }

        
        public string PAY_A { get; set; }
        
        public int PAY_AP { get; set; }
        
        public string PAY_B { get; set; }
        
        public int PAY_BP { get; set; }

        public string SHIPMENT_MODE_NAME { get; set; }
        public string INCO_TERMS_NAME { get; set; }
        public string PRODUCTS_NAME { get; set; }
        public string UNIT { get; set; }
        public int IS_APPROVE { get; set; }

        public int SUBMITED_BIDS { get; set; } = 0;
        public string APPROVAL_ID { get; set; }

    }
}
