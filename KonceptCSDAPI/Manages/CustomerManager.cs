using KonceptCSDAPI.Models.User;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
	public class CustomerManager : ICustomerManager
	{
		private readonly IConfiguration _configuration;
		private IHostingEnvironment _env;
		private MSSQLGateway _MSSQLGateway;
		private List<SqlParameter> param = new List<SqlParameter>();
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private CommonHelper _commonHelper = new CommonHelper();
		private DataTable _dtResp = new DataTable();
		CommonFunctions _CommonFunctions;

		public CustomerManager(IConfiguration configuration, IHostingEnvironment env)
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

		#region Fetch Customer
		public DataTable fetchCustomer(CustomerFilterModel model)
		{
			param.Add(new SqlParameter("User_ID", model.User_ID));
			param.Add(new SqlParameter("Customer_ID", model.Customer_ID));
			param.Add(new SqlParameter("Search", !string.IsNullOrEmpty(model.Search) ? model.Search.Trim() : ""));
			param.Add(new SqlParameter("State_ID", model.State_ID));
			param.Add(new SqlParameter("Is_Active", model.Is_Active));
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_CUSTOMER", param);

			return _dtResp;
		}
		#endregion

		#region Insert Customer,Subscription & Child
		public DataTable insertCustomer(CustomerInsertModel model)
		{
			//Customer Info
			param.Add(new SqlParameter("TBL_CUSTOMER", model.CustomerInfoList));

			//Customer Subscription List
			param.Add(new SqlParameter("TBL_CUSTOMER_SUBSCRIPTION", model.CustomerSubscriptionList));

			//Customer Child List
			param.Add(new SqlParameter("TBL_CUSTOMER_CHILD", model.CustomerChildList));

			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_INSERT_CUSTOMER", param);

			return _dtResp;
		}
		#endregion

	}
}