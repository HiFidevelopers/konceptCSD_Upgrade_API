using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using KonceptSupportLibrary;

namespace KonceptCSDAPI.Models.User
{
    public class CustomerModels
    {
        //[Required(ErrorMessage = "Customer ID is required.")]
        public Int64? User_ID { get; set; } = 0;
        public Int64? Logged_User_ID { get; set; } = 0;
    }

    public class CustomerFilterModel
    {
        public Int64? Customer_ID { get; set; } = 0;
        public string? Search { get; set; } = string.Empty;
        public string Customer_PONO { get; set; } = string.Empty;
        public Int64? Customer_MobileNo { get; set; } = 0;
        public Boolean? Is_Active { get; set; } = true;
        public Int64? Logged_User_ID { get; set; } = 0;
    }


    public class CustomerInsertModel
    {

        //Customer Info
        public Int64? Customer_ID { get; set; } = 0;

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
        [RequiredIf("User_ID", "0", ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [RequiredIf("User_ID", "0", ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        //Customer Subscription List
        [Required(ErrorMessage = "Customer Subscription List is required.")]
        public List<CustomerSubscriptionList> CustomerSubscriptionList { get; set; }

        //Customer Child List
        [Required(ErrorMessage = "Customer Child(Student) List is required.")]
        public List<CustomerChildList> CustomerChildList { get; set; }
    }


    public class CustomerSubscriptionList
    {

        public Int64? Subscription_ID { get; set; } = 0;
        public Int64? User_Group_ID { get; set; } = 0;

        [Required(ErrorMessage = "User Group Access Area ID is required for mapping.")]
        public Int64? User_Group_Access_Area_ID { get; set; } = 0;

        public Boolean? Is_Create { get; set; } = true;
        public Boolean? Is_Retrieve { get; set; } = true;
        public Boolean? Is_Update { get; set; } = true;
        public Boolean? Is_Delete { get; set; } = true;
        public Boolean? Is_Active { get; set; } = true;

    }

    public class CustomerChildList
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
