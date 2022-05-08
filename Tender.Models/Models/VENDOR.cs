using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Range(minimum: 0, maximum: 1)]
        public int SUPPLIER { get; set; }
        [NotMapped]
        public string SUPPLIER_X { get; set; }



        [Display(Name = "As Purchaser")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(minimum: 0, maximum: 1)]
        public int PURCHASER { get; set; }
        [NotMapped]
        public string PURCHASER_X { get; set; }

        [Display(Name = "As Supplier Notification")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(minimum: 0, maximum: 1)]
        public int SUPPLIER_NOTIFY { get; set; }
        [NotMapped]
        public string SUPPLIER_NOTIFY_X { get; set; }

        [Display(Name = "As Purchaser Notification")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(minimum: 0, maximum: 1)]
        public int PURCHASER_NOTIFY { get; set; }
        [NotMapped]
        public string PURCHASER_NOTIFY_X { get; set; }
    }
}
