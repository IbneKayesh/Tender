using System;
using System.ComponentModel.DataAnnotations;

namespace Tender.Models.Models
{
    public class VENDOR_LOGIN
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string VENDOR_EMAIL { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string VENDOR_PASSWD { get; set; }
    }
}
