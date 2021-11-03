using KonceptCSDAPI.Factory;
using KonceptCSDAPI.Managers;
using KonceptCSDAPI.Models.Authentication;
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
    [Route("api/authentication")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        #region Controller Properties
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private IConfiguration _configuration;
        private CommonHelper _objHelper = new CommonHelper();
        private MSSQLGateway _MSSQLGateway;
        private IHostingEnvironment _env;
        public AuthenticationFactory _AuthenticationFactory;
        private IAuthenticationManager _IAuthenticationManager;
        private DataTable _dt;
        private DataRow _dr;
        #endregion Controller Properties

        public AuthenticationController(IConfiguration configuration, IHostingEnvironment env)
        {
            // Get connectin string of current solution
            this._configuration = configuration;
            this._env = env;
            _AuthenticationFactory = new AuthenticationFactory();
            _IAuthenticationManager = _AuthenticationFactory.AuthenticationManager(this._configuration, this._env);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public ServiceResponseModel Signin([FromBody] SiginModel model)
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
                        new Claim("User_ID", Convert.ToString(_dtresp.Rows[0]["User_ID"]), null),
                        new Claim("FirstName", Convert.ToString(_dtresp.Rows[0]["FirstName"]), null),
                        new Claim("Gender",  Convert.ToString(_dtresp.Rows[0]["Gender"]), null),
                        new Claim("Email",  Convert.ToString(_dtresp.Rows[0]["Email"]), null),
                        new Claim(ClaimTypes.Role,  Convert.ToString("User"))
                    };

                    #region GENERATE LOGIN TOKEN

                    string token = this.GenerateToken(claims);

                    #endregion GENERATE LOGIN TOKEN

                    _dt = new DataTable();
                    // Add new columns in respnse model
                    _dr = _dt.NewRow();

                    _dt.Columns.Add("response");
                    _dt.Columns.Add("Login_ID");
                    _dt.Columns.Add("User_ID");
                    _dt.Columns.Add("First_Name");
                    _dt.Columns.Add("Last_Name");
                    _dt.Columns.Add("Email");
                    _dt.Columns.Add("Gender");
                    _dt.Columns.Add("Created_On");

                    DataRow row = _dt.NewRow();
                    row["response"] = "0";
                    row["Login_ID"] = "";
                    row["User_ID"] = "";
                    row["First_Name"] = "";
                    row["Last_Name"] = "";
                    row["Email"] = "";
                    row["Gender"] = "";
                    row["Created_On"] = "";
                    _dt.Rows.Add(row);

                    _dt.Rows[0]["response"] = Convert.ToString(_dtresp.Rows[0]["response"]);
                    _dt.Rows[0]["User_ID"] = Convert.ToString(_dtresp.Rows[0]["User_ID"]);
                    _dt.Rows[0]["FirstName"] = Convert.ToString(_dtresp.Rows[0]["FirstName"]);
                    _dt.Rows[0]["Gender"] = Convert.ToString(_dtresp.Rows[0]["Gender"]);
                    _dt.Rows[0]["Email"] = Convert.ToString(_dtresp.Rows[0]["Email"]);

                    // Convert datatable (data) to dictionary
                    _objResponse.response = 1;
                    _objResponse.data = _objHelper.ConvertTableToDictionary(_dt);
                    _objResponse.sys_message = token;
                }
            }
            return _objResponse;
        }

        public string GenerateToken(List<Claim> claims)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JWTSetting:Key"]));
            SigningCredentials signInCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: this._configuration["JWTSetting:Issuer"],
                audience: this._configuration["JWTSetting:Audience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(this._configuration["JWTSetting:ExpiryInMins"])),
                claims: claims,
                signingCredentials: signInCred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
