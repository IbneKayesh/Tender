using System;
using System.ComponentModel.DataAnnotations;

namespace Tender.Models.Models
{
    public class VENDOR : VENDOR_LOGIN
    {
        [Display(Name = "Your Id")]
        public string VENDOR_ID { get; set; }
        
        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        public string ORGANIZATION_NAME { get; set; }

        [Display(Name = "Country Name")]
        [Required(ErrorMessage = "{0} is required")]
       //[StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string COUNTRY_NAME { get; set; }

        [Display(Name = "As Supplier")]
        [Required(ErrorMessage = "{0} is required")]
        public bool SUPPLIER { get; set; }

        [Display(Name = "As Purchaser")]
        [Required(ErrorMessage = "{0} is required")]
        public bool PURCHASER { get; set; }

        [Display(Name = "As Supplier Notification")]
        [Required(ErrorMessage = "{0} is required")]
        public bool SUPPLIER_NOTIFY { get; set; }

        [Display(Name = "As Purchaser Notification")]
        [Required(ErrorMessage = "{0} is required")]
        public bool PURCHASER_NOTIFY { get; set; }
    }
}
