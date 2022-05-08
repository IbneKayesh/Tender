using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
  public  class CHANGE_PASWD
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Password)]
        public string OPASWD { get; set; }


        [Display(Name = "New Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NPASWD { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 5)]
        [Compare("NPASWD", ErrorMessage = "Confirm password doesn't match, Type again!")]
        [DataType(DataType.Password)]
        public string CPASWD { get; set; }
    }
}
