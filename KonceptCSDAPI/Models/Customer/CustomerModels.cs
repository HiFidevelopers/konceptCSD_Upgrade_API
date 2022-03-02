using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using KonceptSupportLibrary;

namespace KonceptCSDAPI.Models.Customer
{
    public class CustomerModels
    {
        //[Required(ErrorMessage = "Customer ID is required.")]
        public Int64? Customer_ID { get; set; } = 0;
        public Int64? Logged_User_ID { get; set; } = 0;
	}


	public class CustomerFilterModel
	{
		public Int64? Customer_ID { get; set; } = 0;
		public string? Search { get; set; } = string.Empty;
		public Int64? Organization_User_ID { get; set; } = 0;
		public Int64? State_ID { get; set; } = 0;
		public Int64? Package_ID { get; set; } = 0;
		public Boolean? Is_Active { get; set; } = true;
		public Int64? Logged_User_ID { get; set; } = 0;
	}

    public class CustomerSubscriptionFilterModel
    {
        public Int64? Customer_ID { get; set; } = 0;
        public string? Search { get; set; } = string.Empty;
        public Int64? Logged_User_ID { get; set; } = 0;
    }

    public class CustomerChildFilterModel
    {
        public Int64? Customer_ID { get; set; } = 0;
        public string? Search { get; set; } = string.Empty;
        public Int64? Logged_User_ID { get; set; } = 0;
    }


    //Customer Info Insert
    public class CustomerInsertModel
    {
        //Customer (User) Login Info
        [Required(ErrorMessage = "Customer Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Customer Password is required.")]
        public string Password { get; set; }

        //Customer Info
        [Required(ErrorMessage = "Customer Info List is required.")]
        public List<CustomerInfoList> CustomerInfoList { get; set; }

        //Customer Subscription List
        [Required(ErrorMessage = "Customer Subscription List is required.")]
        public List<CustomerSubscriptionList> CustomerSubscriptionList { get; set; }

        //Customer Child List
        [Required(ErrorMessage = "Customer Child(Student) List is required.")]
        public List<CustomerChildList> CustomerChildList { get; set; }

        public Int64? Logged_User_ID { get; set; } = 0;
    }

    //Customer Info Update
    public class CustomerUpdateModel
    {
        //Customer (User) Login Info
        //[Required(ErrorMessage = "Customer Username is required.")]
        //public string Username { get; set; }

        //[Required(ErrorMessage = "Customer Password is required.")]
        //public string Password { get; set; }

        //Customer Info
        [Required(ErrorMessage = "Customer Info List is required.")]
        public List<CustomerInfoUpdateList> CustomerInfoUpdateList { get; set; }

        public Int64? Logged_User_ID { get; set; } = 0;
    }

    //Customer Subscription Update
    public class CustomerSubscriptionUpdateModel
    {

        //Customer Subscription List
        [Required(ErrorMessage = "Customer Subscription List is required.")]
        public List<CustomerSubscriptionUpdateList> CustomerSubscriptionUpdateList { get; set; }

        public Int64? Logged_User_ID { get; set; } = 0;
    }

    //Customer Child Update
    public class CustomerChildUpdateModel
    {
        //Customer Child List
        [Required(ErrorMessage = "Customer Child(Student) List is required.")]
        public List<CustomerChildUpdateList> CustomerChildUpdateList { get; set; }

        public Int64? Logged_User_ID { get; set; } = 0;
    }


    //Customer Info
    public class CustomerInfoList
    {
        public Int64? Customer_ID { get; set; } = 0;
        public string? Father_FirstName { get; set; } = string.Empty;
        public string? Father_LastName { get; set; } = string.Empty;
        public string? Father_Email { get; set; } = string.Empty;
        public string? Father_MobileNo { get; set; } = string.Empty;
        public string? Mother_FirstName { get; set; } = string.Empty;
        public string? Mother_LastName { get; set; } = string.Empty;
        public string? Mother_Email { get; set; } = string.Empty;
        public string? Mother_MobileNo { get; set; } = string.Empty;
        public string? Alt_PhoneNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Address_Other { get; set; } = string.Empty;
        public Int64 Country_ID { get; set; } = 0;
        public Int64 State_ID { get; set; } = 0;
        public string City { get; set; } = string.Empty;
        public string? Zip_Code { get; set; } = string.Empty;
        public Int64? Education_Consultant_ID { get; set; } = 0;
        public string? Important_Notes { get; set; } = string.Empty;
        public Boolean? Is_Active { get; set; } = true;

    }

    public class CustomerInfoUpdateList
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public Int64 Customer_ID { get; set; }
        public string? Father_FirstName { get; set; } = string.Empty;
        public string? Father_LastName { get; set; } = string.Empty;
        public string? Father_Email { get; set; } = string.Empty;
        public string? Father_MobileNo { get; set; } = string.Empty;
        public string? Mother_FirstName { get; set; } = string.Empty;
        public string? Mother_LastName { get; set; } = string.Empty;
        public string? Mother_Email { get; set; } = string.Empty;
        public string? Mother_MobileNo { get; set; } = string.Empty;
        public string? Alt_PhoneNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Address_Other { get; set; } = string.Empty;
        public Int64 Country_ID { get; set; } = 0;
        public Int64 State_ID { get; set; } = 0;
        public string City { get; set; } = string.Empty;
        public string? Zip_Code { get; set; } = string.Empty;
        public Int64? Education_Consultant_ID { get; set; } = 0;
        public string? Important_Notes { get; set; } = string.Empty;
        public Boolean? Is_Active { get; set; } = true;

    }

    //Customer Subscription
    public class CustomerSubscriptionList
    {
        public Int64? Subscription_ID { get; set; } = 0;

        public Int64? Customer_ID { get; set; } = 0;

        [Required(ErrorMessage = "Package ID is required.")]
        public Int64 Package_ID { get; set; }

        [Required(ErrorMessage = "Start_Date is required.")]
        public string Start_Date { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cancellation(End) Date is required.")]
        public string Cancellation_Date { get; set; } = string.Empty;

        [Required(ErrorMessage = "Payment Type ID (Mode Of Payment) is required.")]
        public Int64 Payment_Type_ID { get; set; }
        
        public Boolean? Is_Active { get; set; } = true;
    }

    public class CustomerSubscriptionUpdateList
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public Int64 Customer_ID { get; set; }

        [Required(ErrorMessage = "Subscription ID is required.")]
        public Int64 Subscription_ID { get; set; }

        [Required(ErrorMessage = "Package ID is required.")]
        public Int64 Package_ID { get; set; }

        [Required(ErrorMessage = "Start_Date is required.")]
        public string Start_Date { get; set; } = string.Empty;

		[Required(ErrorMessage = "Cancellation(End) Date is required.")]
		public string Cancellation_Date { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mode Of Payment (Payment_Type_ID) is required.")]
        public Int64 Payment_Type_ID { get; set; } 

        public Boolean? Is_Active { get; set; } = true;
    }

    //Customer Child
    public class CustomerChildList
    {
        public Int64? Customer_Child_ID { get; set; } = 0;

        public Int64? Customer_ID { get; set; } = 0;

        [Required(ErrorMessage = "Customer Child Level (Grade) ID is required.")]
        public Int64 Level_ID { get; set; }

        [Required(ErrorMessage = "Customer Child First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Customer Child Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customer Child Gender is required.")]
        public Int64? Gender { get; set; } = 0;

        public Boolean? Is_Active { get; set; } = true;

    }

    public class CustomerChildUpdateList
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        public Int64 Customer_ID { get; set; }

        [Required(ErrorMessage = "Customer Child ID is required.")]
        public Int64 Customer_Child_ID { get; set; } 

        [Required(ErrorMessage = "Customer Child Level (Grade) ID is required.")]
        public Int64 Level_ID { get; set; }

        [Required(ErrorMessage = "Customer Child First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Customer Child Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Customer Child Gender is required.")]
        public Int64? Gender { get; set; } = 0;

        public Boolean? Is_Active { get; set; } = true;

    }

}
