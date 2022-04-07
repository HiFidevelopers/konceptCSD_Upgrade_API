using System;
using System.ComponentModel.DataAnnotations;
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
		public Boolean? Is_Show_Cancel_Request { get; set; } = true;
		public string? Next_TV_Date { get; set; } = string.Empty;
		public Int64? Logged_User_ID { get; set; } = 0;
	}

	public class CustomerRemarksInsertUpdateModel
	{
		//Customer (Remarks)
		[Required(ErrorMessage = "Customer Remarks Mode is required.")]
		public string Mode { get; set; }
		public Int64? Remarks_ID { get; set; } = 0;

		[Required(ErrorMessage = "Customer ID is required.")]
		public Int64 Customer_ID { get; set; }
		public string Priority_Type { get; set; } = string.Empty; 
		public string Next_Call_Date { get; set; } = string.Empty;
		public string Next_Call_Time { get; set; } = string.Empty;

		[Required(ErrorMessage = "Next TV Date is required.")]
		public string Next_TV_Date { get; set; }

		[Required(ErrorMessage = "Next TV Time is required.")]
		public string Next_TV_Time { get; set; }

		[Required(ErrorMessage = "Customer Remarks is required.")]
		public string Remarks { get; set; }
		public Int64 Logged_User_ID { get; set; }
	}


	public class CustomerRemarksFilterModel
	{
		public Int64? Remarks_ID { get; set; } = 0;
		public Int64 Customer_ID { get; set; }
		public string? Search { get; set; } = string.Empty;
		public Int64? Logged_User_ID { get; set; } = 0;
	}


}
