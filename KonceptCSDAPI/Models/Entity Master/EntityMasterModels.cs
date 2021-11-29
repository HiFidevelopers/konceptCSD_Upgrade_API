using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KonceptCSDAPI.Models.EntityMaster
{
	public class EntityMasterModel
	{
		[Required(ErrorMessage = "SQLFROM is required.")]
		public string SQLFROM { get; set; }

		[Required(ErrorMessage = "SQLBY is required.")]
		public string SQLBY { get; set; }

		public string? SQLPARAM { get; set; } = string.Empty;
		public string? SQLPARAM1 { get; set; } = string.Empty;
		public string? SQLPARAM2 { get; set; } = string.Empty;
		public string? SQLPARAM3 { get; set; } = string.Empty;
		public Int64? Created_By { get; set; } = 0;
	}

}
