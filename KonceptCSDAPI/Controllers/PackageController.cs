using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.Package;
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
	[Route("api/package")]
	[ApiController]
	public class PackageController : ControllerBase
	{
		#region Controller Properties
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private IConfiguration _configuration;
		private CommonHelper _objHelper = new CommonHelper();
		private MSSQLGateway _MSSQLGateway;
		private IHostingEnvironment _env;
		public PackageFactory _PackageFactory;
		private IPackageManager _IPackageManager;
		private DataTable _dt;
		private DataRow _dr;
		CommonFunctions _CommonFunctions;
		#endregion Controller Properties


		public PackageController(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			_PackageFactory = new PackageFactory();
			_IPackageManager = _PackageFactory.PackageManager(this._configuration, this._env);
			_CommonFunctions = new CommonFunctions(configuration, env);
		}


		[HttpPost]
		[Route("fetchpackage")]
		#region Fetch Package

		public ServiceResponseModel fetchPackage([FromBody] PackageFilterModel model)
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

			DataTable _dtresp = _IPackageManager.fetchPackage(model);
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
		[Route("insertupdatepackage")]
		#region Insert Update Package

		public ServiceResponseModel insertUpdatePackage([FromBody] PackageInsertUpdateModel model)
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

			DataTable _dtresp = _IPackageManager.insertUpdatePackage(model);
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
		[Route("deletepackage")]
		#region Delete Package

		public ServiceResponseModel deletePackage([FromBody] PackageDeleteModel model)
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

			DataTable _dtresp = _IPackageManager.deletePackage(model);
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
