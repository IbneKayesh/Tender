using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tender.Models.Models
{
    public class VENDOR_DETAILS : VENDOR
    {
        public VENDOR_DETAILS()
        {
            CHANGE_PASWD = new CHANGE_PASWD();
            VENDOR_FILE = new VENDOR_FILE();
            VENDOR_CATEGORY = new List<VENDOR_CATEGORY>();
            VENDOR_CERTIFICATE = new List<VENDOR_CERTIFICATE>();
            VENDOR_DOCUMENTS = new List<VENDOR_DOCUMENTS>();
            VENDOR_PRODUCTS = new List<VENDOR_PRODUCTS>();
            VENDOR_PRODUCTS_GROUP = new List<VENDOR_PRODUCTS_GROUP>();
            VENDOR_COMPANY = new List<VENDOR_COMPANY>();
        }

        [Display(Name = "Login User Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string VENDOR_USER_ID { get; set; }

        [Display(Name = "1st Contact Person Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NAME { get; set; }

        [Display(Name = "1st Contact Number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NUMBER { get; set; }

        [Display(Name = "2nd Contact Person Name")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NAME2 { get; set; }

        [Display(Name = "2nd Contact Number")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NUMBER2 { get; set; }

        [Display(Name = "3rd Contact Person Name")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NAME3 { get; set; }

        [Display(Name = "3rd Contact Number")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string CONTACT_NUMBER3 { get; set; }


        [Display(Name = "Present Address")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 2)]
        public string ADDRESS_1 { get; set; }

        [Display(Name = "Permanent Address")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string ADDRESS_2 { get; set; }

        [Display(Name = "Establishment Year")]
        [Required(ErrorMessage = "{0} is required")]
        public Nullable<int> YEAR_OF_ESTABLISHMENT { get; set; }

        [Display(Name = "Yearly Turnover")]
        //[Required(ErrorMessage = "{0} is required")]
        [Range(minimum: 1, maximum: 9999999999999, ErrorMessage = "{0} range is {1} and {2}")]
        public Nullable<Int64> YEARLY_TRUNOVER { get; set; }

        [Display(Name = "Number of Employee")]
        //[Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 1)]
        public string NO_OF_EMPLOYEES { get; set; }

        [Display(Name = "TAX/TIN")]
        //[Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string TAX_NUMBER { get; set; }

        [Display(Name = "Trade Number")]
       // [Required(ErrorMessage = "{0} is required")]
        [StringLength(maximumLength: 50, ErrorMessage = "{0} length is between {2} and {1}", MinimumLength = 3)]
        public string TRADE_NUMBER { get; set; }

        public string PROFILE_IMAGE { get; set; }

        public int IS_CONFIRMED { get; set; }
        public int LOCK_UNLOCK { get; set; }

        public bool IS_APPROVE { get; set; }


        [NotMapped]
        public CHANGE_PASWD CHANGE_PASWD { get; set; }
        [NotMapped]
        public VENDOR_FILE VENDOR_FILE { get; set; }

        public virtual List<VENDOR_CATEGORY> VENDOR_CATEGORY { get; set; }
        public virtual List<VENDOR_CERTIFICATE> VENDOR_CERTIFICATE { get; set; }
        public virtual List<VENDOR_DOCUMENTS> VENDOR_DOCUMENTS { get; set; }
        public virtual List<VENDOR_PRODUCTS> VENDOR_PRODUCTS { get; set; }
        public virtual List<VENDOR_PRODUCTS_GROUP> VENDOR_PRODUCTS_GROUP { get; set; }
        public virtual List<VENDOR_FILE> VENDOR_DOCUMENTS_LIST { get; set; }
        public virtual List<VENDOR_COMPANY> VENDOR_COMPANY { get; set; }





        public VENDOR_DETAILS getAll()
        {
            VENDOR_DETAILS obj = new VENDOR_DETAILS();
            obj.ORGANIZATION_NAME = "My XYZ Con;";
            obj.VENDOR_USER_ID = "xyz123";
            obj.CONTACT_NUMBER = "0185861262588";
            obj.VENDOR_EMAIL = "example@ex.com";
            obj.ADDRESS_1 = "Merul Badda, Dit project";
            obj.ADDRESS_2 = "Merul Badda, Dit project";
            //obj.PURCHASER = 1;
            //obj.PURCHASER_NOTIFY = 1;
            //obj.SUPPLIER = 0;
            //obj.SUPPLIER = 0;
          
           // obj.VENDOR_DOCUMENTS = new VENDOR_DOCUMENTS().getAll();
          //  obj.VENDOR_PRODUCTS = new VENDOR_PRODUCTS().getAll();
            return obj;
        }

    }
}
