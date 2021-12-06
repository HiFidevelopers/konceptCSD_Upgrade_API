using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.User;
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
	[Route("api/user")]
	[ApiController]
	public class UserController : ControllerBase
	{
		#region Controller Properties
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private IConfiguration _configuration;
		private CommonHelper _objHelper = new CommonHelper();
		private MSSQLGateway _MSSQLGateway;
		private IHostingEnvironment _env;
		public UserFactory _UserFactory;
		private IUserManager _IUserManager;
		private DataTable _dt;
		private DataRow _dr;
		CommonFunctions _CommonFunctions;
		#endregion Controller Properties


		public UserController(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			_UserFactory = new UserFactory();
			_IUserManager = _UserFactory.UserManager(this._configuration, this._env);
			_CommonFunctions = new CommonFunctions(configuration, env);
		}


		[HttpPost]
		[Route("fetchuser")]
		#region Fetch User

		public ServiceResponseModel fetchUser([FromBody] UserFilterModel model)
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

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));

			DataTable _dtresp = _IUserManager.fetchUser(model);
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


		[HttpPost]
		[Route("insertupdateuser")]
		#region Insert Update User

		public ServiceResponseModel insertUpdateUser([FromBody] UserInsertUpdateModel model)
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

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));

			DataTable _dtresp = _IUserManager.insertUpdateUser(model);
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



		[HttpPost]
		[Route("fetchusergroup")]
		#region Fetch User Group

		public ServiceResponseModel fetchUserGroup([FromBody] UserGroupFilterModel model)
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

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));

			DataTable _dtresp = _IUserManager.fetchUserGroup(model);
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



		[HttpPost]
		[Route("fetchusergroupmapping")]
		#region Fetch User Group

		public ServiceResponseModel fetchUserGroupMapping([FromBody] UserGroupFilterModel model)
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

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));

			DataTable _dtresp = _IUserManager.fetchUserGroupMapping(model);
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



		[HttpPost]
		[Route("insertupdateusergroup")]
		#region Insert Update User

		public ServiceResponseModel insertUpdateUserGroup([FromBody] UserGroupInsertUpdateModel model)
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

			model.Logged_User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));

			DataTable _dtresp = _IUserManager.insertUpdateUserGroup(model);
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
