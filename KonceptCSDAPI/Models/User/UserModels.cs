using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

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


	public class UserTypeInsertUpdateModel
	{
		public Int64? User_Type_ID { get; set; } = 0;

		[Required(ErrorMessage = "User Type is required.")]
		public string User_Type { get; set; }

		public int? Is_Active { get; set; } = null;
	}


}
