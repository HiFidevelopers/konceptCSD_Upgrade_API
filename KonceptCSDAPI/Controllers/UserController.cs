using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using KonceptSupportLibrary;
using KonceptCSDAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Data;
using KonceptCSDAPI.Models.User;

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
        #endregion Controller Properties

        public UserController(IConfiguration configuration, IHostingEnvironment env)
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
        }

        List<SqlParameter> _param = new List<SqlParameter>();


        //================================= User =================================//

        #region Insert User Type
        [HttpPost("insert-user-type")]
        public ServiceResponseModel InsertUserType([FromBody] UserTypeInsertUpdateModel utm)
        {
            try
            {
                // Check validation
                if (!ModelState.IsValid)
                {
                    _objResponse.response = 0;
                    _objResponse.sys_message = _objHelper.GetModelErrorMessages(ModelState);
                    return _objResponse;
                }

                _param.Add(new SqlParameter("Mode", "INSERT"));
                _param.Add(new SqlParameter("User_Type", utm.User_Type.Trim()));
                _param.Add(new SqlParameter("Is_Active", utm.Is_Active));
                _param.Add(new SqlParameter("Logged_User_ID", Convert.ToInt32(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"))));
                _objResponse = UserRequest("[APP_INSERT_UPDATE_USER_TYPE]", _param);
            }
            catch (Exception ex)
            {
                _objResponse.response = 0;
                _objResponse.sys_message = ex.Message;
            }
            return _objResponse;
        }
        #endregion

        #region Update User Type
        [HttpPost("update-user-type")]
        public ServiceResponseModel UpdateUserType([FromBody] UserTypeInsertUpdateModel utm)
        {
            try
            {
                // Check validation
                if (!ModelState.IsValid)
                {
                    _objResponse.response = 0;
                    _objResponse.sys_message = _objHelper.GetModelErrorMessages(ModelState);
                    return _objResponse;
                }

                _param.Add(new SqlParameter("Mode", "UPDATE"));
                _param.Add(new SqlParameter("User_Type", utm.User_Type.Trim()));
                _param.Add(new SqlParameter("Is_Active", utm.Is_Active));
                _param.Add(new SqlParameter("Logged_User_ID", Convert.ToInt32(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"))));
                _objResponse = UserRequest("[APP_INSERT_UPDATE_USER_TYPE]", _param);
            }
            catch (Exception ex)
            {
                _objResponse.response = 0;
                _objResponse.sys_message = ex.Message;
            }
            return _objResponse;
        }
        #endregion


        #region Fetch User
        [HttpPost("fetch-user")]
        public ServiceResponseModel FetchUser([FromBody] UserFilterModel um)
        {
            try
            {
                // Check validation
                if (!ModelState.IsValid)
                {
                    _objResponse.response = 0;
                    _objResponse.sys_message = _objHelper.GetModelErrorMessages(ModelState);
                    return _objResponse;
                }
                _param.Add(new SqlParameter("User_ID", um.User_ID));
                _param.Add(new SqlParameter("Search", um.Search.Trim()));
                _param.Add(new SqlParameter("User_Type_ID", um.User_Type_ID));
                _param.Add(new SqlParameter("Is_Active", um.Is_Active));
                _param.Add(new SqlParameter("Logged_User_ID", Convert.ToInt32(_objHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"))));
                _objResponse = UserRequest("[FETCH_USER]", _param);
            }
            catch (Exception ex)
            {
                _objResponse.response = 0;
                _objResponse.sys_message = ex.Message;
            }
            return _objResponse;
        }
        #endregion


        // Non API Route Methods
        #region User Request and Response
        private ServiceResponseModel UserRequest(string procedureName, List<SqlParameter> sp)
        {
            // Execute procedure with parameters for post data
            DataTable dtresp = _MSSQLGateway.ExecuteProcedure(Convert.ToString(procedureName), sp);
            if (_objHelper.checkDBResponse(dtresp))
            {
                if (Convert.ToInt32(Convert.ToString(dtresp.Rows[0]["response"])) <= 0)
                {
                    _objResponse.response = Convert.ToInt32(Convert.ToString(dtresp.Rows[0]["response"]));
                    _objResponse.sys_message = Convert.ToString(dtresp.Rows[0]["message"].ToString());
                }
                else
                {
                    _objResponse.response = Convert.ToInt32(dtresp.Rows[0]["response"]);
                    _objResponse.data = _objHelper.ConvertTableToDictionary(dtresp);
                    _objResponse.sys_message = Convert.ToString(dtresp.Rows[0]["message"].ToString());
                }
            }
            return _objResponse;
        }

        private ServiceResponseModel UserResponse(string procedureName, List<SqlParameter> sp)
        {
            // Execute procedure with parameters for get data
            DataTable dtresp = _MSSQLGateway.ExecuteProcedure(Convert.ToString(procedureName), sp);
            if (_objHelper.checkDBResponse(dtresp))
            {
                if (Convert.ToInt32(Convert.ToString(dtresp.Rows[0]["response"])) <= 0)
                {
                    _objResponse.response = Convert.ToInt32(Convert.ToString(dtresp.Rows[0]["response"]));
                    _objResponse.sys_message = Convert.ToString(dtresp.Rows[0]["message"].ToString());
                }
                else
                {
                    _objResponse.response = Convert.ToInt32(dtresp.Rows[0]["response"]); ;
                    _objResponse.data = _objHelper.ConvertTableToDictionary(dtresp);
                }
            }
            return _objResponse;
        }
        #endregion

    }
}
