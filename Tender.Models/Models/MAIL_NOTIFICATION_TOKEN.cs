using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tender.Models.Models
{
  public class MAIL_NOTIFICATION_TOKEN
    {
        public string VENDOR_EMAIL { get; set; }
        public int TOKEN { get; set; }
        public DateTime ADDED_DATE { get; set; }
        public string TOKEN_TYPE { get; set; }
    }
}
