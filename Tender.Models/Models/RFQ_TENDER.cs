using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_TENDER : COMMON
    {
        public string RFQ_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Vendor Id")]
        public string VENDOR_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Saler/Buyer")]
        public int SELL_BUY { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Importer type")]
        public string LOCAL_IMPORT { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int RE_BID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int LOWER_RATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Start Date")]
        public DateTime START_DATE { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("End Date")]
        public DateTime END_DATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Products")]
        public string PRODUCTS_ID { get; set; }
        //[Required(ErrorMessage = "{0} is required")]
        //[StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 1)]
        [DisplayName("Product Details")]
        public string PRODUCTS_DESC { get; set; }
        //[Required(ErrorMessage = "{0} is required")]
        [DisplayName("Rate")]
        public decimal PRODUCTS_RATE { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Quantity")]
        public decimal PRODUCTS_QUANTITY { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Last Delivery Date")]
        public DateTime LAST_DELIVERY_DATE { get; set; }

        [DisplayName("Partial Shipment")]
        public int  PARTIAL_SHIPMENT { get; set; }
        [DisplayName("Shipmetn Mode")]
        public int SHIPMENT_MODE { get; set; } = 0;
        [DisplayName("Port")]
        public int PORT_ID { get; set; } = 0;
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 1)]
        [DisplayName("Delivery Address")]
        public string DELIVERY_ADDRESS { get; set; }
        //[Required(ErrorMessage = "{0} is required")]
       // [StringLength(maximumLength: 30, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 1)]
        public string RECEIVER_NAME { get; set; }
        //[Required(ErrorMessage = "{0} is required")]
       // [StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 1)]
        public string RECEIVER_DETAILS { get; set; }

        public int COST_EX_INC { get; set; }
        public string INCO_TERMS { get; set; }
        [DisplayName("Currency Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string CURRENCY_NAME { get; set; }
        [DisplayName("Currency Rate")]
        public decimal CURRENCY_RATE { get; set; }

        [DisplayName("Pyament Mode A")]
        [Required(ErrorMessage = "{0} is required")]
        public string PAY_A { get; set; }
        [DisplayName("Payment Mode A % ")]
        [Required(ErrorMessage = "{0} is required")]

        public int PAY_AP { get; set; }
        [DisplayName("Payment Mode B")]
        [Required(ErrorMessage = "{0} is required")]
        public string PAY_B { get; set; }
        [DisplayName("Payment Mode B %")]
        [Required(ErrorMessage = "{0} is required")]
        public int PAY_BP { get; set; }

        public DateTime ADDED_DATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [DisplayName("Company")]
        public string COMPANY_ID { get; set; }

        public int IS_APPROVE { get; set; }

        public virtual List<RFQ_TNDR_DOCUMENTS> RFQ_TNDR_DOCUMENTS { get; set; }



    }
}
