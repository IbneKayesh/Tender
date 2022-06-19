using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tender.Models.Models
{
    public class TNDR_PRODUCTS
    {
        public string PRODUCTS_ID { get; set; }
        [Display(Name = "Product Unit")]
        [Required(ErrorMessage = "{0} is required")]
        public string UNIT { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "{0} is required")]
        public string PRODUCTS_NAME { get; set; }
        public string IMAGE_PATH { get; set; }
        public string GROUP_ID { get; set; }

     //   [ImageFile(Width = 100, Height = 100, ErrorMessage = "Please upload image 100x100")]
        public HttpPostedFileBase ProductPicture { get; set; }

    }
}
