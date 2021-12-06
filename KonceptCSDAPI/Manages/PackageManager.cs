using KonceptCSDAPI.Models.Package;
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
    public class PackageManager : IPackageManager
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private MSSQLGateway _MSSQLGateway;
        private List<SqlParameter> param = new List<SqlParameter>();
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private CommonHelper _commonHelper = new CommonHelper();
        private DataTable _dtResp = new DataTable();
        CommonFunctions _CommonFunctions;

        public PackageManager(IConfiguration configuration, IHostingEnvironment env)
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
            _CommonFunctions = new CommonFunctions(configuration, env);
        }

        #region Fetch Package
        public DataTable fetchPackage(PackageFilterModel model)
        {
            param.Add(new SqlParameter("Package_ID", model.Package_ID));
            param.Add(new SqlParameter("Search", model.Search.Trim()));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));
            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_PACKAGE", param);

            return _dtResp;
        }
        #endregion


        #region Insert Update Package
        public DataTable insertUpdatePackage(PackageInsertUpdateModel model)
        {
            //Package Info
            if (model.Package_ID > 0)
            {
                param.Add(new SqlParameter("Mode", "UPDATE"));
            }
            else
            {
                param.Add(new SqlParameter("Mode", "INSERT"));
            }
            param.Add(new SqlParameter("Package_ID", model.Package_ID));
            param.Add(new SqlParameter("Package", model.Package));
            param.Add(new SqlParameter("Code", model.Code));
            param.Add(new SqlParameter("Currency_ID", model.Currency_ID));
            param.Add(new SqlParameter("Package_Price", model.Package_Price));
            param.Add(new SqlParameter("Session_Type_Period", model.Session_Type_Period));
            param.Add(new SqlParameter("Session_Number_Visits", model.Session_Number_Visits));
            param.Add(new SqlParameter("Session_Reports_Period", model.Session_Reports_Period));
            param.Add(new SqlParameter("Session_Hours", model.Session_Hours));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));

            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_INSERT_UPDATE_PACKAGE", param);

            return _dtResp;
        }
        #endregion

    }
}