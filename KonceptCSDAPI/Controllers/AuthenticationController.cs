using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.Authentication;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
    [Route("api/authentication")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        #region Controller Properties
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private IConfiguration _configuration;
        private CommonHelper _objHelper = new CommonHelper();
        private MSSQLGateway _MSSQLGateway;
        private IDistributedCache _distributedCache;
        private IHostingEnvironment _env;
        public AuthenticationFactory _AuthenticationFactory;
        private IAuthenticationManager _IAuthenticationManager;
        private DataTable _dt;
        private DataRow _dr;
        CommonFunctions _CommonFunctions;
        #endregion Controller Properties

        public AuthenticationController(IConfiguration configuration, IHostingEnvironment env)
        {
            // Get connectin string of current solution
            this._configuration = configuration;
            this._env = env;
            _AuthenticationFactory = new AuthenticationFactory();
            _IAuthenticationManager = _AuthenticationFactory.AuthenticationManager(this._configuration, this._env);
            _CommonFunctions = new CommonFunctions(configuration, env);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public ServiceResponseModel Signin([FromBody] 
        SiginModel model)
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

            DataTable _dtresp = _IAuthenticationManager.Signin(model);
            if (_objHelper.checkDBResponse(_dtresp))
            {
                if (Convert.ToString(_dtresp.Rows[0]["response"]) == "0")
                {
                    _objResponse.response = 0;
                    _objResponse.sys_message = Convert.ToString(_dtresp.Rows[0]["message"]);
                }
                else
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim("Login_ID", Convert.ToString(_dtresp.Rows[0]["Login_ID"])),
                        new Claim("User_ID", Convert.ToString(_dtresp.Rows[0]["User_ID"])),
                        new Claim("FullName", Convert.ToString(_dtresp.Rows[0]["FullName"])),
                        new Claim("Gender",  Convert.ToString(_dtresp.Rows[0]["Gender"])),
                        new Claim("User_Type",  Convert.ToString(_dtresp.Rows[0]["User_Type"])),
                        new Claim("Profile_Pic",  Convert.ToString(_dtresp.Rows[0]["Profile_Pic"])),
                        new Claim("User_Group_ID",  Convert.ToString(_dtresp.Rows[0]["User_Group_ID"])),
                        new Claim("User_Group_Name",  Convert.ToString(_dtresp.Rows[0]["User_Group_Name"])),
                        new Claim("Email",  Convert.ToString(_dtresp.Rows[0]["Email"])),
                        new Claim(ClaimTypes.Role,  Convert.ToString("User"))
                    };

                    #region GENERATE LOGIN TOKEN

                    string token = _CommonFunctions.GenerateToken(claims);

                    #endregion GENERATE LOGIN TOKEN

                    _dt = new DataTable();
                    // Add new columns in respnse model
                    _dr = _dt.NewRow();

                    _dt.Columns.Add("response");
                    _dt.Columns.Add("Login_ID");
                    _dt.Columns.Add("User_ID");
                    _dt.Columns.Add("FullName");
                    _dt.Columns.Add("User_Type");
                    _dt.Columns.Add("Profile_Pic");
                    _dt.Columns.Add("User_Group_ID");
                    _dt.Columns.Add("User_Group_Name");
                    _dt.Columns.Add("Gender");
                    _dt.Columns.Add("Email");

                    DataRow row = _dt.NewRow();
                    row["response"] = "0";
                    row["Login_ID"] = "";
                    row["User_ID"] = "";
                    row["FullName"] = "";
                    row["User_Type"] = "";
                    row["Profile_Pic"] = "";
                    row["User_Group_ID"] = "";
                    row["User_Group_Name"] = "";
                    row["Gender"] = "";
                    row["Email"] = "";
                    _dt.Rows.Add(row);

                    _dt.Rows[0]["response"] = Convert.ToString(_dtresp.Rows[0]["response"]);
                    _dt.Rows[0]["Login_ID"] = Convert.ToString(_dtresp.Rows[0]["Login_ID"]);
                    _dt.Rows[0]["User_ID"] = Convert.ToString(_dtresp.Rows[0]["User_ID"]);
                    _dt.Rows[0]["FullName"] = Convert.ToString(_dtresp.Rows[0]["FullName"]);
                    _dt.Rows[0]["User_Type"] = Convert.ToString(_dtresp.Rows[0]["User_Type"]);
                    _dt.Rows[0]["Profile_Pic"] = Convert.ToString(_dtresp.Rows[0]["Profile_Pic"]);
                    _dt.Rows[0]["User_Group_ID"] = Convert.ToString(_dtresp.Rows[0]["User_Group_ID"]);
                    _dt.Rows[0]["User_Group_Name"] = Convert.ToString(_dtresp.Rows[0]["User_Group_Name"]);
                    _dt.Rows[0]["Gender"] = Convert.ToString(_dtresp.Rows[0]["Gender"]);
                    _dt.Rows[0]["Email"] = Convert.ToString(_dtresp.Rows[0]["Email"]);

                    // Convert datatable (data) to dictionary
                    _objResponse.response = 1;
                    _objResponse.data = _objHelper.ConvertTableToDictionary(_dt);
                    _objResponse.sys_message = token;

                }
            }
            else
            {
                _objResponse.response = 0;
                _objResponse.sys_message = "Invalid username or password";
            }
            return _objResponse;
        }
    }

}
