using KonceptCSDAPI.Models.Authentication;
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
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private MSSQLGateway _MSSQLGateway;
        private List<SqlParameter> param = new List<SqlParameter>();
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private CommonHelper _commonHelper = new CommonHelper();
        private DataTable _dtResp = new DataTable();

        public AuthenticationManager(IConfiguration configuration, IHostingEnvironment env)
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

        public DataTable Signin(SiginModel modell)
        {
            param.Add(new SqlParameter("Login", modell.username));
            param.Add(new SqlParameter("Password", modell.password));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_AUTH_USER_LOGIN", param);
            return _dtResp;
        }
    }
}