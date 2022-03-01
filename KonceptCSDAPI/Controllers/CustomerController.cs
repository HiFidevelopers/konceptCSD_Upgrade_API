using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.Customer;
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
	[Route("api/customer")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		#region Controller Properties
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private IConfiguration _configuration;
		private CommonHelper _objHelper = new CommonHelper();
		private MSSQLGateway _MSSQLGateway;
		private IHostingEnvironment _env;
		public CustomerFactory _CustomerFactory;
		private ICustomerManager _ICustomerManager;
		private DataTable _dt;
		private DataRow _dr;
		CommonFunctions _CommonFunctions;
		#endregion Controller Properties

		public CustomerController(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			_CustomerFactory = new CustomerFactory();
			_ICustomerManager = _CustomerFactory.CustomerManager(this._configuration, this._env);
			_CommonFunctions = new CommonFunctions(configuration, env);
		}

		[HttpPost]
		[Route("fetchcustomer")]
		#region Fetch Customer

		public ServiceResponseModel fetchCustomer([FromBody] CustomerFilterModel model)
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

			DataTable _dtresp = _ICustomerManager.fetchCustomer(model);
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
		[Route("insertcustomer")]
		#region Insert Customer

		public ServiceResponseModel insertCustomer([FromBody] CustomerInsertModel model)
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

			DataTable _dtresp = _ICustomerManager.insertCustomer(model);
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
		[Route("updatecustomer")]
		#region Update Customer

		public ServiceResponseModel updateCustomer([FromBody] CustomerUpdateModel model)
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

			DataTable _dtresp = _ICustomerManager.updateCustomer(model);
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
		[Route("fetchsubscription")]
		#region Fetch Customer Subscription

		public ServiceResponseModel fetchSubscription([FromBody] CustomerSubscriptionFilterModel model)
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

			DataTable _dtresp = _ICustomerManager.fetchSubscription(model);
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
		[Route("updatecustomersubscription")]
		#region Update Customer Subscription

		public ServiceResponseModel updateCustomerSubscription([FromBody] CustomerSubscriptionUpdateModel model)
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

			DataTable _dtresp = _ICustomerManager.updateCustomerSubscription(model);
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
		[Route("fetchcustomerchild")]
		#region Fetch Customer Child

		public ServiceResponseModel fetchCustomerChild([FromBody] CustomerChildFilterModel model)
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

			DataTable _dtresp = _ICustomerManager.fetchCustomerChild(model);
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
		[Route("updatecustomerchild")]
		#region Update Customer Child

		public ServiceResponseModel updateCustomerChild([FromBody] CustomerChildUpdateModel model)
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

			DataTable _dtresp = _ICustomerManager.updateCustomerChild(model);
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
