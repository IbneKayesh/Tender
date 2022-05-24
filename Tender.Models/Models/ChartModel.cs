using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Tender.Models.Models
{
    public class ChartModel
    {
		public ChartModel(string label, double y)
		{
			this.Label = label;
			this.Y = y ;
		}

		public string Label = "";

		public Nullable<double> Y = null;
	}
}
