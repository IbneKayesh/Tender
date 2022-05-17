using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
    public class RFQ_TENDER
    {
        public string RFQ_NUMBER { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string VENDOR_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int SELL_BUY { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string LOCAL_IMPORT { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int RE_BID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int LOWER_RATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime START_DATE { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public DateTime END_DATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string PRODUCTS_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string PRODUCTS_DESC { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public decimal PRODUCTS_RATE { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public decimal PRODUCTS_QUANTITY { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime LAST_DELIVERY_DATE { get; set; }

        public int  PARTIAL_SHIPMENT { get; set; }
        public int SHIPMENT_MODE { get; set; } = 0;
        public int PORT_ID { get; set; } = 0;
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string DELIVERY_ADDRESS { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 30, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string RECEIVER_NAME { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 90, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string RECEIVER_DETAILS { get; set; }

        public int COST_EX_INC { get; set; }
        public string INCO_TERMS { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string CURRENCY_NAME { get; set; }
        public decimal CURRENCY_RATE { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string PAY_A { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int PAY_AP { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string PAY_B { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int PAY_BP { get; set; }


        public virtual List<RFQ_TNDR_DOCUMENTS> RFQ_TNDR_DOCUMENTS { get; set; }



    }
}
