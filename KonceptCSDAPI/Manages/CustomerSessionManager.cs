using KonceptCSDAPI.Models.CustomerSession;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace KonceptCSDAPI.Managers
{
	public class CustomerSessionManager : ICustomerSessionManager
	{
		private readonly IConfiguration _configuration;
		private IHostingEnvironment _env;
		private MSSQLGateway _MSSQLGateway;
		private List<SqlParameter> param = new List<SqlParameter>();
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private CommonHelper _commonHelper = new CommonHelper();
		private DataTable _dtResp = new DataTable();
		CommonFunctions _CommonFunctions;

		public CustomerSessionManager(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			if (_env.IsDevelopment() || _env.EnvironmentName.ToLower() == "localhost")
			{
				this._MSSQLGateway = new MSSQLGateway(this._configuration.GetConnectionString("ConnectionDev"));
			}
			else if (_env.IsProduction())
			{
				this._MSSQLGateway = new MSSQLGateway(this._configuration.GetConnectionString("ConnectionPro"));
			}
			_CommonFunctions = new CommonFunctions(configuration, env);
		}

		#region Fetch Customer Due Session
		public DataTable fetchCustomerDueSession(CustomerDueSessionFilterModel model)
		{
			param.Add(new SqlParameter("Package_ID", model.Package_ID));
			param.Add(new SqlParameter("Search", !string.IsNullOrEmpty(model.Search) ? model.Search.Trim() : ""));
			param.Add(new SqlParameter("Is_Show_Cancel_Request", model.Is_Show_Cancel_Request));
			param.Add(new SqlParameter("Next_TV", model.Next_TV_Date));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));
			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_DUE_SESSIONS", param);

			return _dtResp;
		}
		#endregion

		#region Insert Update Customer Remarks
		public DataTable insertUpdateCustomerRemarks(CustomerRemarksInsertUpdateModel model)
		{
			if (model.Remarks_ID > 0)
			{
				param.Add(new SqlParameter("Mode", "UPDATE"));
			}
			else
			{
				param.Add(new SqlParameter("Mode", "INSERT"));
			}
			param.Add(new SqlParameter("Remarks_ID", model.Remarks_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Priority_Type", model.Priority_Type));
			param.Add(new SqlParameter("Next_Call_Date", model.Next_Call_Date));
			param.Add(new SqlParameter("Next_Call_Time", model.Next_Call_Time));
			param.Add(new SqlParameter("Next_TV_Date", model.Next_TV_Date));
			param.Add(new SqlParameter("Next_TV_Time", model.Next_TV_Time));
			param.Add(new SqlParameter("Remarks", model.Remarks));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_INSERT_UPDATE_CUSTOMER_REMARKS]", param);

			return _dtResp;
		}
		#endregion

		#region Fetch Customer Remarks
		public DataTable fetchCustomerRemarks(CustomerRemarksFilterModel model)
		{
			param.Add(new SqlParameter("Remarks_ID", model.Remarks_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Search", !string.IsNullOrEmpty(model.Search) ? model.Search.Trim() : ""));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));
			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_FETCH_CUSTOMER_REMARKS]", param);

			return _dtResp;
		}
		#endregion

		#region Fetch Customer Description History
		public DataTable fetchCustomerDescriptionHistory(CustomerDescriptionHistoryFilterModel model)
		{
			param.Add(new SqlParameter("Description_History_ID", model.Description_History_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Search", !string.IsNullOrEmpty(model.Search) ? model.Search.Trim() : ""));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));
			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_FETCH_CUSTOMER_DESCRIPTION_HISTORY]", param);

			return _dtResp;
		}
		#endregion

		#region Insert Update Customer Description History
		public DataTable insertUpdateCustomerDescriptionHistory(CustomerDescriptionHistoryInsertUpdateModel model)
		{
			if (model.Description_History_ID > 0)
			{
				param.Add(new SqlParameter("Mode", "UPDATE"));
			}
			else
			{
				param.Add(new SqlParameter("Mode", "INSERT"));
			}
			param.Add(new SqlParameter("Description_History_ID", model.Description_History_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Subject", model.Subject));
			param.Add(new SqlParameter("Description", model.Description));
			param.Add(new SqlParameter("Attachment", model.Attachment));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_INSERT_UPDATE_CUSTOMER_DESCRIPTION_HISTORY]", param);

			return _dtResp;
		}
		#endregion

		#region Fetch Customer Request
		public DataTable fetchCustomerRequest(CustomerRequestFilterModel model)
		{
			param.Add(new SqlParameter("Request_ID", model.Request_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Search", !string.IsNullOrEmpty(model.Search) ? model.Search.Trim() : ""));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));
			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_FETCH_CUSTOMER_REQUEST]", param);

			return _dtResp;
		}
		#endregion

		#region Insert Update Customer Request
		public DataTable insertUpdateCustomerRequest(CustomerRequestInsertUpdateModel model)
		{
			if (model.Request_ID > 0)
			{
				param.Add(new SqlParameter("Mode", "UPDATE"));
			}
			else
			{
				param.Add(new SqlParameter("Mode", "INSERT"));
			}
			//param.Add(new SqlParameter("Request_ID", model.Request_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Subject", model.Subject));
			param.Add(new SqlParameter("Description", model.Description));
			param.Add(new SqlParameter("Attachment", model.Attachment));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("[APP_INSERT_UPDATE_CUSTOMER_DESCRIPTION_HISTORY]", param);

			return _dtResp;
		}
		#endregion

	}
}