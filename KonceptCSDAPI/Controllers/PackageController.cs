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
using KonceptCSDAPI.Models.Package;

namespace KonceptCSDAPI.Controllers
{
    [Produces("application/json")]
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
        #endregion Controller Properties

        public PackageController(IConfiguration configuration, IHostingEnvironment env)
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


        //================================= Package =================================//

        #region Insert Package
        [HttpPost("insertpackage")]
        public ServiceResponseModel InsertPackage([FromBody] PackageInsertUpdateModel pm)
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
                _param.Add(new SqlParameter("Package", pm.Package.Trim()));
                _param.Add(new SqlParameter("Price", pm.Price.Trim()));
                _param.Add(new SqlParameter("Code", pm.Code.Trim()));
                _param.Add(new SqlParameter("Is_Active", pm.Is_Active));
                _param.Add(new SqlParameter("Logged_User_ID", "1"));
                _objResponse = PackageRequest("[APP_INSERT_UPDATE_PACKAGE]", _param);
            }
            catch (Exception ex)
            {
                _objResponse.response = 0;
                _objResponse.sys_message = ex.Message;
            }
            return _objResponse;
        }
        #endregion

        #region Update Package
        [HttpPost("update-package")]
        public ServiceResponseModel UpdatePackage([FromBody] PackageInsertUpdateModel pm)
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
                _param.Add(new SqlParameter("Package_ID", pm.Package_ID));
                _param.Add(new SqlParameter("Package", pm.Package.Trim()));
                _param.Add(new SqlParameter("Price", pm.Price.Trim()));
                _param.Add(new SqlParameter("Code", pm.Code.Trim()));
                _param.Add(new SqlParameter("Is_Active", pm.Is_Active));
                _param.Add(new SqlParameter("Logged_User_ID",  "1"));
                _objResponse = PackageRequest("[APP_INSERT_UPDATE_PACKAGE]", _param);
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
        #region Package Request and Response
        private ServiceResponseModel PackageRequest(string procedureName, List<SqlParameter> sp)
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

        private ServiceResponseModel PackageResponse(string procedureName, List<SqlParameter> sp)
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
