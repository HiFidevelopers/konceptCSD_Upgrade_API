using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.EntityMaster;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Controllers
{
	[Produces("application/json")]
	[Route("api/entitymaster")]
	//[Authorize]
    public class EntityMasterController : ControllerBase
	{
		#region Controller Properties
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private IConfiguration _configuration;
		private CommonHelper _objHelper = new CommonHelper();
		private MSSQLGateway _MSSQLGateway;
		private IHostingEnvironment _env;
		public EntityMasterFactory _EntityMasterFactory;
		private IEntityMasterManager _IEntityMasterManager;
		private DataTable _dt;
		private DataRow _dr;
		CommonFunctions _CommonFunctions;
		#endregion Controller Properties


		public EntityMasterController(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			_EntityMasterFactory = new EntityMasterFactory();
			_IEntityMasterManager = _EntityMasterFactory.EntityMasterManager(this._configuration, this._env);
			_CommonFunctions = new CommonFunctions(configuration, env);
		}


		[HttpPost]
		[Route("fetchentitymaster")]
		#region Fetch Entity Master

		public ServiceResponseModel fetchentitymaster([FromBody] EntityMasterModel model)
		{
			#region DATA VALIDATION
			if (model == null)
			{
				_objResponse.sys_message = _objHelper.GetModelErrorMessages(ModelState);
				_objResponse.response = 0;
				return _objResponse;
			}
			else
			{
				if (!ModelState.IsValid)
				{
					_objResponse.sys_message = "input model is not supplied.";
					_objResponse.response = 0;
					return _objResponse;
				}
			}
			#endregion

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "Login_ID"));

			DataTable _dtresp = _IEntityMasterManager.fetchentitymaster(model);
			if (_objHelper.checkDBResponse(_dtresp))
			{
				if (Convert.ToString(_dtresp.Rows[0]["response"]) == "0")
				{
					_objResponse.response = 0;
					_objResponse.sys_message = Convert.ToString(_dtresp.Rows[0]["message"]);
				}
				else
				{
					_objResponse.response = 1;
					_objResponse.data = _objHelper.ConvertTableToDictionary(_dtresp);
				}
			}
			return _objResponse;
		}
		#endregion


	}
}
