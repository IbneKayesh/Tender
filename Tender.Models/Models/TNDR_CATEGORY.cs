using System.Collections.Generic;

namespace Tender.Models.Models
{
    public class TNDR_CATEGORY
    {
        public string CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }

        public List<TNDR_CATEGORY> getAll()
        {
            return new List<TNDR_CATEGORY> { new TNDR_CATEGORY { CATEGORY_ID = "10", CATEGORY_NAME = "Test Category" } };
        }
    }
}
