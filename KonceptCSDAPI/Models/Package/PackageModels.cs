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
	}

	public class PackageFilterModel
	{
		public Int64? Package_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public int? Is_Active { get; set; } = null;
	}


	public class PackageInsertUpdateModel
	{
		public Int64? Package_ID { get; set; } = 0;

		[Required(ErrorMessage = "Package Name is required.")]
		public string Package { get; set; }

		[Required(ErrorMessage = "Package Price is required.")]
		public string Price { get; set; }

		[Required(ErrorMessage = "Package Code is required.")]
		public string Code { get; set; }
		public int? Is_Active { get; set; } = null;
	}
}
