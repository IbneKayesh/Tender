using System;
using System.ComponentModel.DataAnnotations;

namespace Tender.Models.Models
{
    public class VENDOR : VENDOR_LOGIN
    {

        public string VENDOR_ID { get; set; }

        [Required]
        public string ORGANIZATION_NAME { get; set; }
        [Required]
        public string COUNTRY_NAME { get; set; }

        [Required]
        public bool SUPPLIER { get; set; }
        [Required]
        public bool PURCHASER { get; set; }

        [Required]
        public bool SUPPLIER_NOTIFY { get; set; }
        [Required]
        public bool PURCHASER_NOTIFY { get; set; }
    }
}
