using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Models.User
{
	public class UserModels
	{
		//[Required(ErrorMessage = "User ID is required.")]
		public Int64? User_ID { get; set; } = 0;
	}

	public class UserFilterModel
	{
		public Int64? User_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public Int64? User_Type_ID { get; set; } = 0;
		public int? Is_Active { get; set; } = null;
	}
}
