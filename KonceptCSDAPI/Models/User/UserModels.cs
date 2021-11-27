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
		public string User_Type { get; set; } = string.Empty;
		public Int64? User_Group_ID { get; set; } = 0;
		public Boolean? Is_Active { get; set; } = true;
	}


	public class UserInsertUpdateModel
	{

		//User Info
		public Int64? User_ID { get; set; } = 0;

		[Required(ErrorMessage = "User Type is required.")]
		public string User_Type { get; set; }

		[Required(ErrorMessage = "Parent(Organization) User is required.")]
		public Int64 Parent_User_ID { get; set; }

		public string? User_Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "User Group is required.")]
		public Int64 User_Group_ID { get; set; }
		public string? FullName { get; set; } = string.Empty;

		[Required(ErrorMessage = "First Name is required.")]
		public string FirstName { get; set; }
		public string? LastName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Gender is required.")]
		public int Gender { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		public string Email { get; set; }
		public Boolean? Is_Email_Verify { get; set; } = true;

		[Required(ErrorMessage = "MobileNo is required.")]
		public string MobileNo { get; set; }

		public Boolean? Is_Mobile_Verify { get; set; } = true;
		public string? Valid_till { get; set; } = string.Empty;
		public Boolean? Is_Active { get; set; } = true;


		//User Login Info
		[Required(ErrorMessage = "Username is required.")]
		public string Username { get; set; }


		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }


		//User Profile Info
		public string Profile_Pic { get; set; }
		public string? Address { get; set; } = string.Empty;
		public string? Address_Other { get; set; } = string.Empty;
		public string? City { get; set; } = string.Empty;
		public Int64? State_ID { get; set; } = 0;
		public Int64? Country_ID { get; set; } = 0;
		public string? Zip_Code { get; set; } = string.Empty;
		public string? Facebook_Profile_URL { get; set; } = string.Empty;
		public string? LinkedIn_Profile_URL { get; set; } = string.Empty;

	}


	public class UserGroupFilterModel
	{
		public Int64? User_Group_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public string? User_Group_Name { get; set; } = string.Empty;
		public Boolean? Is_Predefined { get; set; } = true;
		public Boolean? Is_Active { get; set; } = true;
	}


	public class UserGroupInsertUpdateModel
	{
		public Int64? User_Group_ID { get; set; } = 0;

		[Required(ErrorMessage = "User Group Name is required.")]
		public string User_Group_Name { get; set; }

		[Required(ErrorMessage = "User Group Description is required.")]
		public string User_Group_Description { get; set; }
		public Boolean? Is_Predefined { get; set; } = false;
		public Boolean? Is_Active { get; set; } = true;

		//User Group Access Area Mapping List
		[Required(ErrorMessage = "User Group Access Area Mapping List is required.")]
		public List<AccessAreaList> AccessAreaList { get; set; }

	}


	public class AccessAreaList
	{

		public Int64? User_Group_Access_Area_Mapping_ID { get; set; } = 0;
		public Int64? User_Group_ID { get; set; } = 0;

		[Required(ErrorMessage = "User Group Access Area ID is required for mapping.")]
		public Int64? User_Group_Access_Area_ID { get; set; } = 0;

		public Boolean? Is_Create { get; set; } = true;
		public Boolean? Is_Retrieve { get; set; } = true;
		public Boolean? Is_Update { get; set; } = true;
		public Boolean? Is_Delete { get; set; } = true;
		public Boolean? Is_Active { get; set; } = true;

	}

}
