using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime SUBMIT_DATE { get; set; }
        public string PRODUCTS_ID { get; set; }
        public string PRODUCTS_DESC { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public decimal PRODUCTS_RATE { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public decimal PRODUCTS_QUANTITY { get; set; }
        public int SHIPMENT_MODE { get; set; }
        public int PORT_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string LOADING_ADDRESS { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string SENDER_NAME { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        public string SENDER_DETAILS { get; set; }

        [NotMapped]
        public string TND_VENDOR_ID { get; set; }
        [NotMapped]
        public string SELL_BUY { get; set; }
        [NotMapped]
        public string LOCAL_IMPORT { get; set; }
        [NotMapped]
        public string RE_BID { get; set; }
        [NotMapped]
        public string LOWER_RATE { get; set; }
        [NotMapped]
        public DateTime START_DATE { get; set; }
        [NotMapped]
        public DateTime END_DATE { get; set; }
        [NotMapped]
        public DateTime LAST_DELIVERY_DATE { get; set; }
        [NotMapped]
        public string TND_PRODUCTS_NAME { get; set; }
        [NotMapped]
        public string UNIT { get; set; }
        [NotMapped]
        public string TND_PRODUCTS_DESC { get; set; }
        [NotMapped]
        public decimal TND_PRODUCTS_RATE { get; set; }
        [NotMapped]
        public decimal TND_PRODUCTS_QTY { get; set; }
        [NotMapped]
        public string PARTIAL_SHIPMENT { get; set; }
        [NotMapped]
        public string TENDER_SHIPMENT_MODE { get; set; }
        [NotMapped]
        public string TENDER_PORT_ID { get; set; }
        [NotMapped]
        public string DELIVERY_ADDRESS { get; set; }
        [NotMapped]
        public string RECEIVER_NAME { get; set; }
        [NotMapped]
        public string RECEIVER_DETAILS { get; set; }
        [NotMapped]
        public string COST_EX_INC { get; set; }
        [NotMapped]
        public string INCO_TERMS_NAME { get; set; }
        [NotMapped]
        public string CURRENCY_NAME { get; set; }
        [NotMapped]
        public string CURRENCY_RATE { get; set; }
        [NotMapped]
        public string PAY_A { get; set; }
        [NotMapped]
        public int PAY_AP { get; set; }
        [NotMapped]
        public string PAY_B { get; set; }
        [NotMapped]
        public int PAY_BP { get; set; }
        [NotMapped]
        public int TOTAL_BIDDING { get; set; } = 0;

        //FOR QUTATION SHIPMENT

        [NotMapped]
        public string Q_SHIPMENT_MODE { get; set; }
        [NotMapped]
        public string Q_PORT_NAME { get; set; }
        [NotMapped]
        public string VENDOR_NAME { get; set; }
        public string APPROVAL_ID { get; set; }



    }
}
