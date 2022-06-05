using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KonceptCSDAPI.Models.Package
{
	public class PackageModels
	{
		//[Required(ErrorMessage = "Package ID is required.")]
		public Int64? Package_ID { get; set; } = 0;

		public Int64? Logged_User_ID { get; set; } = 0;
	}

	public class PackageFilterModel
	{
		public Int64? Package_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public Boolean? Is_Public { get; set; } = null;
		public Boolean? Is_Active { get; set; } = null;
		public Int64? Logged_User_ID { get; set; } = 0;
	}


	public class PackageInsertUpdateModel
	{
		public Int64? Package_ID { get; set; } = 0;

		[Required(ErrorMessage = "Package Name is required.")]
		public string Package { get; set; }

		[Required(ErrorMessage = "Package Currency is required.")]
		public Int64? Currency_ID { get; set; } = 0;

		[Required(ErrorMessage = "Package Price is required.")]
		public string Package_Price { get; set; }

		public string? Code { get; set; } = string.Empty;
		

		[Required(ErrorMessage = "Package Session Type Period is required.")]
		public Int64 Session_Type_Period { get; set; }

		[Required(ErrorMessage = "Package Session Number of visits is required.")]
		public Int64 Session_Number_Visits { get; set; }

		[Required(ErrorMessage = "Package Session Reports Period is required.")]
		public Int64 Session_Reports_Period { get; set; }

		[Required(ErrorMessage = "Package Session Hours is required.")]
		public Int64 Session_Hours { get; set; }

		public Boolean? Is_Public { get; set; } = false;

		public Boolean? Is_Active { get; set; } = true;

		public Int64? Logged_User_ID { get; set; } = 0;
	}


	public class PackageDeleteModel
	{
		public Int64 Package_ID { get; set; }
		public Boolean Is_Deleted { get; set; }
		public Int64? Logged_User_ID { get; set; } = 0;
	}
}
