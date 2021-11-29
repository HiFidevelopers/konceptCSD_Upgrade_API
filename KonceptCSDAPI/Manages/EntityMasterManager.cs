using KonceptCSDAPI.Models.EntityMaster;
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
	public class EntityMasterManager : IEntityMasterManager
	{
		private readonly IConfiguration _configuration;
		private IHostingEnvironment _env;
		private MSSQLGateway _MSSQLGateway;
		private List<SqlParameter> param = new List<SqlParameter>();
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private CommonHelper _commonHelper = new CommonHelper();
		private DataTable _dtResp = new DataTable();
		CommonFunctions _CommonFunctions;

		public EntityMasterManager(IConfiguration configuration, IHostingEnvironment env)
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

		#region Fetch Entity Master
		public DataTable fetchentitymaster(EntityMasterModel model)
		{
			param.Add(new SqlParameter("SQLFROM", model.SQLFROM.Trim()));
			param.Add(new SqlParameter("SQLBY", model.SQLBY.Trim()));
			param.Add(new SqlParameter("SQLPARAM", model.SQLPARAM.Trim()));
			param.Add(new SqlParameter("SQLPARAM1", model.SQLPARAM.Trim()));
			param.Add(new SqlParameter("SQLPARAM2", model.SQLPARAM.Trim()));
			param.Add(new SqlParameter("SQLPARAM3", model.SQLPARAM.Trim()));
			param.Add(new SqlParameter("Logged_User_ID", model.Created_By));

			//param.Add(new SqlParameter("Logged_User_ID", Convert.ToInt32(_commonHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"))));

			DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_ENTITY_MASTER", param);

			return _dtResp;
		}
		#endregion

	}
}