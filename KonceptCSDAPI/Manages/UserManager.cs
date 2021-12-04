using KonceptCSDAPI.Models.User;
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
    public class UserManager : IUserManager
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        private MSSQLGateway _MSSQLGateway;
        private List<SqlParameter> param = new List<SqlParameter>();
        private ServiceResponseModel _objResponse = new ServiceResponseModel();
        private CommonHelper _commonHelper = new CommonHelper();
        private DataTable _dtResp = new DataTable();
        CommonFunctions _CommonFunctions;

        public UserManager(IConfiguration configuration, IHostingEnvironment env)
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

        #region Fetch User
        public DataTable fetchUser(UserFilterModel model)
        {
            param.Add(new SqlParameter("User_ID", model.User_ID));
            param.Add(new SqlParameter("Search", model.Search.Trim()));
            param.Add(new SqlParameter("User_Type", model.User_Type));
            param.Add(new SqlParameter("User_Group_ID", model.User_Group_ID));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));
            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_USER", param);

            return _dtResp;
        }
        #endregion


        #region Insert Update User
        public DataTable insertUpdateUser(UserInsertUpdateModel model)
        {
            //User Info
            if (model.User_ID > 0)
            {
                param.Add(new SqlParameter("Mode", "UPDATE"));
            }
            else
            {
                param.Add(new SqlParameter("Mode", "INSERT"));
            }
            param.Add(new SqlParameter("User_ID", model.User_ID));
            param.Add(new SqlParameter("User_Type", model.User_Type));
            param.Add(new SqlParameter("Parent_User_ID", model.Parent_User_ID));
            param.Add(new SqlParameter("User_Description", model.User_Description));
            param.Add(new SqlParameter("User_Group_ID", model.User_Group_ID));
            param.Add(new SqlParameter("FullName", model.FullName));

            param.Add(new SqlParameter("FirstName", model.FirstName.Trim()));
            param.Add(new SqlParameter("LastName", model.LastName));
            param.Add(new SqlParameter("Gender", model.Gender));
            param.Add(new SqlParameter("Email", model.Email.Trim()));
            param.Add(new SqlParameter("Is_Email_Verify", model.Is_Email_Verify));
            param.Add(new SqlParameter("MobileNo", model.MobileNo));
            param.Add(new SqlParameter("Is_Mobile_Verify", model.Is_Mobile_Verify));
            param.Add(new SqlParameter("Valid_till", model.Valid_till));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));


            //User Login
            param.Add(new SqlParameter("Username", model.Username.Trim()));
            param.Add(new SqlParameter("Password", _CommonFunctions.ConvertToSHA512(model.Password.Trim())));

            //User Profile
            param.Add(new SqlParameter("Profile_Pic", model.Profile_Pic));
            param.Add(new SqlParameter("Address", model.Address));
            param.Add(new SqlParameter("Address_Other", model.Address_Other));
            param.Add(new SqlParameter("City", model.City));
            param.Add(new SqlParameter("State_ID", model.State_ID));
            param.Add(new SqlParameter("Country_ID", model.Country_ID));
            param.Add(new SqlParameter("Zip_Code", model.Zip_Code));
            param.Add(new SqlParameter("Facebook_Profile_URL", model.Facebook_Profile_URL));
            param.Add(new SqlParameter("LinkedIn_Profile_URL", model.LinkedIn_Profile_URL));

            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_INSERT_UPDATE_USER", param);

            return _dtResp;
        }
        #endregion


        #region Fetch User Group
        public DataTable fetchUserGroup(UserGroupFilterModel model)
        {
            param.Add(new SqlParameter("User_Group_ID", model.User_Group_ID));
            param.Add(new SqlParameter("Search", model.Search.Trim()));
            param.Add(new SqlParameter("User_Group_Name", model.User_Group_Name.Trim()));
            param.Add(new SqlParameter("Is_Predefined", model.Is_Predefined));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));
            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_FETCH_USER_GROUP_WITH_ACCESS_AREA_WITH_MAPPING", param);

            return _dtResp;
        }
        #endregion


        #region Insert Update User Group
        public DataTable insertUpdateUserGroup(UserGroupInsertUpdateModel model)
        {
            //User Info
            if (model.User_Group_ID > 0)
            {
                param.Add(new SqlParameter("Mode", "UPDATE"));
            }
            else
            {
                param.Add(new SqlParameter("Mode", "INSERT"));
            }
            param.Add(new SqlParameter("User_Group_ID", model.User_Group_ID));
            param.Add(new SqlParameter("User_Group_Name", model.User_Group_Name));
            param.Add(new SqlParameter("User_Group_Description", model.User_Group_Description));
            param.Add(new SqlParameter("Is_Predefined", model.Is_Predefined));
            param.Add(new SqlParameter("Is_Active", model.Is_Active));


            //USER_GROUP_ACCESS_AREA_MAPPING
            param.Add(new SqlParameter("TBL_USER_GROUP_ACCESS_AREA_MAPPING", _commonHelper.ConvertListToTable(model.AccessAreaList)));


            param.Add(new SqlParameter("Logged_User_ID", model.Logged_User_ID));

            DataTable _dtResp = _MSSQLGateway.ExecuteProcedure("APP_INSERT_UPDATE_USER_GROUP", param);

            return _dtResp;
        }
        #endregion

    }
}