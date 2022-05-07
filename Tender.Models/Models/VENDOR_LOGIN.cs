using System;
using System.ComponentModel.DataAnnotations;

namespace Tender.Models.Models
{
    public class VENDOR_LOGIN
    {

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        public string VENDOR_EMAIL { get; set; }
        

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        public string VENDOR_PASSWD { get; set; }
    }
}
