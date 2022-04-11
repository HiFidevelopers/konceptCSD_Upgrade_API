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


	public class CustomerRemarksFilterModel
	{
		public Int64? Remarks_ID { get; set; } = 0;
		public Int64 Customer_ID { get; set; }
		public string? Search { get; set; } = string.Empty;
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
		public string? Priority_Type { get; set; } = string.Empty;
		public string? Next_Call_Date { get; set; } = string.Empty;
		public string? Next_Call_Time { get; set; } = string.Empty;

		[Required(ErrorMessage = "Next TV Date is required.")]
		public string Next_TV_Date { get; set; }

		[Required(ErrorMessage = "Next TV Time is required.")]
		public string Next_TV_Time { get; set; }

		[Required(ErrorMessage = "Customer Remarks is required.")]
		public string Remarks { get; set; }
		public Int64 Logged_User_ID { get; set; }
	}

	public class CustomerDescriptionHistoryFilterModel
	{
		public Int64? Description_History_ID { get; set; } = 0;
		public Int64 Customer_ID { get; set; }
		public string? Search { get; set; } = string.Empty;
		public Int64? Logged_User_ID { get; set; } = 0;
	}

	public class CustomerDescriptionHistoryInsertUpdateModel
	{
		//Customer (Description History)
		[Required(ErrorMessage = "Customer Description History Mode is required.")]
		public string Mode { get; set; }
		public Int64? Description_History_ID { get; set; } = 0;

		[Required(ErrorMessage = "Customer ID is required.")]
		public Int64 Customer_ID { get; set; }
		public string? Subject { get; set; } = string.Empty;

		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }
		public string? Attachment { get; set; } = string.Empty;
		public Int64 Logged_User_ID { get; set; }
	}

	public class CustomerRequestFilterModel
	{
		public Int64? Request_ID { get; set; } = 0;
		public Int64 Customer_ID { get; set; }
		public string? Search { get; set; } = string.Empty;
		public Int64? Logged_User_ID { get; set; } = 0;
	}

	public class CustomerRequestInsertUpdateModel
	{
		//Customer (Remarks)
		[Required(ErrorMessage = "Customer Request Mode is required.")]
		public string Mode { get; set; }
		public Int64? Request_ID { get; set; } = 0;

		[Required(ErrorMessage = "Customer ID is required.")]
		public Int64 Customer_ID { get; set; }

		[Required(ErrorMessage = "Subject is required.")]
		public string Subject { get; set; }

		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; }
		public string? Attachment { get; set; } = string.Empty;

		[Required(ErrorMessage = "Request Type is required.")]
		public Int64 Request_Type_ID { get; set; }

		[Required(ErrorMessage = "Request Status is required.")]
		public Int64 Request_Status_ID { get; set; }
		public Int64? Assigned_By_User_ID { get; set; } = 0;
		public Int64? Assigned_To_User_ID { get; set; } = 0;
		public Int64 Logged_User_ID { get; set; }
	}
}
