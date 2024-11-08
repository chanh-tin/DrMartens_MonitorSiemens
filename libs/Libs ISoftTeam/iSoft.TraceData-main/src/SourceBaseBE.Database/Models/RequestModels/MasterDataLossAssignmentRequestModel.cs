using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.Models.RequestModels
{
	public class MasterDataLossAssignmentRequestModel
	{
		public bool? Workshop { get; set; }
		public bool? Line { get; set; }
		public bool? OeePoint { get; set; }	
		public bool? LossName { get; set; }
		public bool? LossPosition { get; set; }
		public bool? LossDescription { get; set; }
		public bool? LossGroup { get; set; }

	}
	 

}
