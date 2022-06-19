using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.CustomerSession;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Security.Claims;



namespace KonceptCSDAPI.Controllers
{
	[Route("api/customersession")]
	[ApiController]
	public class CustomerSessionController : ControllerBase
	{
		#region Controller Properties
		private ServiceResponseModel _objResponse = new ServiceResponseModel();
		private IConfiguration _configuration;
		private CommonHelper _objHelper = new CommonHelper();
		private MSSQLGateway _MSSQLGateway;
		private IHostingEnvironment _env;
		public CustomerSessionFactory _CustomerSessionFactory;
		private ICustomerSessionManager _ICustomerSessionManager;
		private DataTable _dt;
		private DataRow _dr;
		CommonFunctions _CommonFunctions;
		#endregion Controller Properties

		public CustomerSessionController(IConfiguration configuration, IHostingEnvironment env)
		{
			// Get connectin string of current solution
			this._configuration = configuration;
			this._env = env;
			_CustomerSessionFactory = new CustomerSessionFactory();
			_ICustomerSessionManager = _CustomerSessionFactory.CustomerSessionManager(this._configuration, this._env);
			_CommonFunctions = new CommonFunctions(configuration, env);
		}

		[HttpPost]
		[Route("fetchcustomerduesession")]
		#region Fetch Customer Due Session

		public ServiceResponseModel fetchCustomerDueSession([FromBody] CustomerDueSessionFilterModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.fetchCustomerDueSession(model);
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
		[Route("fetchcustomerremarks")]
		#region Fetch Customer Remarks

		public ServiceResponseModel fetchCustomerRemarks([FromBody] CustomerRemarksFilterModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.fetchCustomerRemarks(model);
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
		[Route("insertUpdateCustomerRemarks")]
		#region Insert Update Customer Remarks

		public ServiceResponseModel insertUpdateCustomerRemarks([FromBody] CustomerRemarksInsertUpdateModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.insertUpdateCustomerRemarks(model);
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
		[Route("fetchcustomerdescriptionhistory")]
		#region Fetch Customer Description History

		public ServiceResponseModel fetchCustomerDescriptionHistory([FromBody] CustomerDescriptionHistoryFilterModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.fetchCustomerDescriptionHistory(model);
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
		[Route("insertupdatecustomerdescriptionhistory")]
		#region Insert Update Customer Description History

		public ServiceResponseModel insertUpdateCustomerDescriptionHistory([FromBody] CustomerDescriptionHistoryInsertUpdateModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.insertUpdateCustomerDescriptionHistory(model);
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
		[Route("fetchcustomerrequest")]
		#region Fetch Customer Request

		public ServiceResponseModel fetchCustomerRequest([FromBody] CustomerRequestFilterModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.fetchCustomerRequest(model);
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
		[Route("insertupdatecustomerrequest")]
		#region Insert Update Customer Request

		public ServiceResponseModel insertUpdateCustomerRequest([FromBody] CustomerRequestInsertUpdateModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.insertUpdateCustomerRequest(model);
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
		[Route("fetchtutorslotsavailability")]
		#region Fetch Tutor Slots Availability

		public ServiceResponseModel fetchTutorSlotsAvailability([FromBody] TutorSlotsAvailabilityFilterModel model)
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

			DataTable _dtresp = _ICustomerSessionManager.fetchTutorSlotsAvailability(model);
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
		[Route("insertWeeklySlotAvailibility")]
		#region Fetch Tutor Slots Availability

		public ServiceResponseModel insertWeeklySlotAvailibility([FromBody] WeeklySlotAvailabilityDataModel model)
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

			for (int i = 0; i < model.phaseExecutions.PRE.Count; i++)
			{
				WeeklySlotAvailabilityModel _model = new WeeklySlotAvailabilityModel();
				_model.User_ID = Convert.ToInt64(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"));
				_model.Start_Time = Convert.ToString(model.phaseExecutions.PRE[i].StartTime);
				_model.End_Time = Convert.ToString(model.phaseExecutions.PRE[i].EndTime);
				_model.WeekDay_ID = Convert.ToInt64(model.weekday);
				_model.Work_Type = "Work_Hours";

				DataTable _dtresp = _ICustomerSessionManager.insertWeeklySlotAvailibility(_model);
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
			}
			return _objResponse;
		}
		#endregion

	}
}
