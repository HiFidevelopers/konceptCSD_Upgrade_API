using KonceptCSDAPI.Models.EntityMaster;
using KonceptCSDAPI.Models.NavigationMenuModel;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
    public class EntityMasterManager : IEntityMasterManager
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private MSSQLGateway _MSSQLGateway;
        private List<SqlParameter> param = new List<SqlParameter>();
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private CommonHelper _commonHelper = new CommonHelper();
        private DataTable _dtResp = new DataTable();
        CommonFunctions _CommonFunctions;

        public EntityMasterManager(IConfiguration configuration, IHostingEnvironment env)
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

        #region Fetch Entity Master
        public DataTable fetchentitymaster(EntityMasterModel model)
        {
            param.Add(new SqlParameter("SQLFROM", model.SQLFROM.Trim()));
            param.Add(new SqlParameter("SQLBY", model.SQLBY.Trim()));
            param.Add(new SqlParameter("SQLPARAM", model.SQLPARAM.Trim()));
            param.Add(new SqlParameter("SQLPARAM1", model.SQLPARAM.Trim()));
            param.Add(new SqlParameter("SQLPARAM2", model.SQLPARAM.Trim()));
            param.Add(new SqlParameter("SQLPARAM3", model.SQLPARAM.Trim()));
            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            //param.Add(new SqlParameter("Logged_User_ID", Convert.ToInt32(_commonHelper.GetTokenData(HttpContext.User.Identity as ClaimsIdentity, "User_ID"))));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_ENTITY_MASTER", param);

            return _dtResp;
        }
        #endregion

        #region Fetch Navigation Menu
        public DataTable fetchNavigationMenu(NavigationMenuParameterModel model)
        {
            param.Add(new SqlParameter("Role_ID", model.roleid));

            List<NavigationMenuModel> _NavigationMenuModel = new List<NavigationMenuModel>();

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_MENU_BY_ROLEID", param);

            if (_commonHelper.checkDBResponse(_dtResp))
            {
                if (Convert.ToString(_dtResp.Rows[0]["response"]) == "0")
                {
                    _objResponse.response = 0;
                    _objResponse.sys_message = Convert.ToString(_dtResp.Rows[0]["message"]);
                }
                else
                {
                    _NavigationMenuModel = GetNavigationMenuList(_dtResp);
                    _objResponse.response = 1;

                    var json = JsonConvert.SerializeObject(_NavigationMenuModel);

                    _objResponse.sys_message = json.Replace(@"\", " ");
                }
            }
            return _dtResp;
        }
        public List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            pro.SetValue(objT, row[pro.Name]);
                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
        public List<NavigationMenuModel> GetNavigationMenuList(DataTable dt)
        {
            List<NavigationMenuModel> Studentlist = new List<NavigationMenuModel>();
            Studentlist = ConvertToList<NavigationMenuModel>(dt);
            return Studentlist;
        }
        #endregion

    }
}