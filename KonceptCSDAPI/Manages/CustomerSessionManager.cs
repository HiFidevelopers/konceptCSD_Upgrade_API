using KonceptCSDAPI.Models.CustomerSession;
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
			param.Add(new SqlParameter("Next_TV", model.Next_TV)); 
			param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));
			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_DUE_SESSIONS", param);

			return _dtResp;
		}
		#endregion
		 
	}
}