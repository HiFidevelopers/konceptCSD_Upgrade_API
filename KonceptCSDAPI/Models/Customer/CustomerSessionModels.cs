using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using KonceptSupportLibrary;

namespace KonceptCSDAPI.Models.CustomerSession
{
    public class CustomerSessionModels
    {
        //[Required(ErrorMessage = "Customer ID is required.")]
        public Int64? Customer_ID { get; set; } = 0;
        public Int64? Logged_User_ID { get; set; } = 0;
	}


	public class CustomerDueSessionFilterModel
	{
		public Int64? Package_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public Int64? Is_Show_Cancel_Request { get; set; } = 0;
		public Int64? Next_TV { get; set; } = 0;	
		public Int64? Logged_User_ID { get; set; } = 0;
	}

}
