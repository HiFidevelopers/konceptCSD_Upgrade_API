using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KonceptSupportLibrary
{
    public class CommonHelper
    {
        // Function for get token data
        public string GetTokenData(ClaimsIdentity identity, string key)
        {
            return identity.FindFirst(key).Value;
        }

        // Function for get error messages
        public string GetModelErrorMessages(ModelStateDictionary errors)
        {
            string messages = "";
            foreach (var item in errors)
            {
                for (int j = 0; j < item.Value.Errors.Count; j++)
                {
                    //messages += $"{item.Key.ToString().ToUpper()}: {item.Value.Errors[j].ErrorMessage} <br />";
                    messages += $"<strong>{item.Value.Errors[j].ErrorMessage}</strong><br/>";
                }
            }
            return messages;
        }

        // Function for convert datatable to dictionary
        public List<Dictionary<string, string>> ConvertTableToDictionary(DataTable dt)
        {
            List<Dictionary<string, string>> _list = new List<Dictionary<string, string>>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dictionary<string, string> _row = new Dictionary<string, string>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    _row.Add(dt.Columns[j].ColumnName, dt.Rows[i][dt.Columns[j].ColumnName].ToString());
                }
                _list.Add(_row);
            }
            return _list;
        }

        public DataTable ConvertListToTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        // Overloaded Function for Null Check DataTable
        public Boolean checkDBNullResponse(DataTable _dt)
        {
            return (_dt == null) ? false : true;
        }

        // Overloaded Function for Null Check DataSet
        public Boolean checkDBNullResponse(DataSet _ds)
        {
            return (_ds == null) ? false : true;
        }

        // Overloaded Function for complete check DataTable
        public Boolean checkDBResponse(DataTable _dt)
        {
            return (_dt == null || _dt.Rows.Count == 0) ? false : true;
        }

        // Overloaded Function for complete check DataSet
        public Boolean checkDBResponse(DataSet _ds)
        {
            return (_ds == null || _ds.Tables.Count == 0) ? false : true;
        }


        public bool SendEmail(string emailId, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(emailId);

                //if (email.CCMailID != null && email.CCMailID != "")
                //    mail.CC.Add(email.CCMailID);

                mail.From = new MailAddress("no-reply@keykoncept.com");
                mail.Subject = subject;
                string Body = body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "mail.keykoncept.com"; //Or Your SMTP Server Address
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                ("no-reply@keykoncept.com", "koncept2021");

                //Or your Smtp Email ID and Password
                smtp.EnableSsl = false;
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                //Log.Error("Email Sent Error: " + ex.Message,false);
                return false;
            }
        }

        // Send SMS
        public string SendSMS(string mobilenumber, string msg)
        {
            try
            {
                String smsservicetype = "singlemsg";
                String query = "username=" + HttpUtility.UrlEncode("") + "&password=" + HttpUtility.UrlEncode("") + "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) + "&content=" + HttpUtility.UrlEncode(msg) + "&mobileno=" + HttpUtility.UrlEncode(mobilenumber) + "&senderid=" + HttpUtility.UrlEncode("");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("");
                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                request.KeepAlive = false;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        // Generate OTP
        public string generateOTP(int length = 6)
        {
            Random generator = new Random();

            return generator.Next((int)Math.Pow(10, (length - 1)), (int)Math.Pow(10, length) - 1).ToString();
        }

        // Generate Rendom String
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        // Function for POST and GET httpWebRequests
        public async Task<ServiceResponseModel> UserWebRequest(string URL, string Method, string token)
        {
            ServiceResponseModel rm = new ServiceResponseModel();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = Method;
                request.Headers.Add("Authorization", token);
                var response = await request.GetResponseAsync();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                rm.data = JsonConvert.DeserializeObject<ServiceResponseModel>(responseString);
                if (!string.IsNullOrEmpty(Convert.ToString(rm.data)) || rm.data != null)
                {
                    rm.response_code = "100";
                    rm.sys_message = "Data";
                }
                return rm;
            }
            catch (Exception ex)
            {
                return rm;
            }
        }

        // Function for POST and GET httpWebRequests
        public async Task<ServiceResponseModel> UserWebRequestWithBody(string URL, string Method, string body)
        {
            ServiceResponseModel rm = new ServiceResponseModel();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = Method;

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = body;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    rm.response = 1;
                    rm.data = JsonConvert.DeserializeObject<ServiceResponseModel>(result);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(rm.data)) || rm.data != null)
                {
                    rm.response = 1;
                    rm.sys_message = "Data has been generated successfully.";
                }
                return rm;
            }
            catch (Exception ex)
            {
                return rm;
            }
        }

        // Function for POST and GET httpWebRequests with Token
        public async Task<ServiceResponseModel> UserWebRequestWithBody(string URL, string Method, string token, string body)
        {
            ServiceResponseModel rm = new ServiceResponseModel();
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = Method;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("Authorization", "Bearer " + token);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = body;
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    rm.response = 1;
                    rm.data = JsonConvert.DeserializeObject<ServiceResponseModel>(result);
                }

                if (!string.IsNullOrEmpty(Convert.ToString(rm.data)) || rm.data != null)
                {
                    rm.response = 1;
                    rm.sys_message = "Data has been generated successfully.";
                }
                return rm;
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    Console.WriteLine("Error code: {0}", httpResponse.StatusCode);
                    using (Stream data = response.GetResponseStream())
                    {
                        string text = new StreamReader(data).ReadToEnd();
                        rm.response_code = text;
                    }
                }
                return rm;
            }
            catch (Exception ex)
            {
                return rm;
            }
        }
        // Function for POST and GET httpWebRequests with Token
        public ServiceResponseModel UserWebRequestWithoutToken(string URL, string Method)
        {
            ServiceResponseModel rm = new ServiceResponseModel();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = Method;
                var response = request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                rm.data = JsonConvert.DeserializeObject<ServiceResponseModel>(responseString);
                return rm;
            }
            catch (Exception ex)
            {
                return rm;
            }
        }

        // Function for check response model is null
        public Boolean checkResponseModel(ServiceResponseModel rm)
        {
            return (rm == null) ? false : true;
        }

        // Function for check response model rows is null
        public Boolean checkResponseModel(ServiceResponseModelRows rm)
        {
            return (rm == null) ? false : true;
        }

        // Function for check JArray is null
        public Boolean checkJArray(JArray jA)
        {
            return (jA == null) ? false : true;
        }

        // Convert To SHA512
        public string ConvertToSHA512(string _string)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                _string = Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(_string)));
            }
            return _string;
        }

        // ReCaptchaPassed
        public bool ReCaptchaPassed(string gRecaptchaResponse, string secret)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}");
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                CaptchaResModel res = JsonConvert.DeserializeObject<CaptchaResModel>(responseString);
                if (response.StatusCode.ToString() != "OK" || res.success == false)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region DateTime functions
        public DateTime ConvertESTToIST(DateTime timeInEST_)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeInEST_, TimeZoneInfo.Local.Id, "India Standard Time");
        }

        public DateTime ConvertESTToEST(DateTime timeInEST_)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(timeInEST_,TimeZoneInfo.Local.Id, "Eastern Africa Time");
        }

        public DateTime ToIST(DateTime dateTime_)
        {
            return ConvertESTToIST(dateTime_);
        }
        #endregion
    }
}







//public DateTime ToServerTime(this DateTime dateTime_)
//{
//    return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime_, "India Standard Time", TimeZoneInfo.Local.Id);
//}

//public DateTime ToServerTimeStartOfDay(this DateTime dateTime_)
//{
//    DateTime dt = new DateTime(dateTime_.Year, dateTime_.Month, dateTime_.Day, 0, 0, 0);
//    return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, "India Standard Time", TimeZoneInfo.Local.Id);
//}

//public DateTime ToServerTimeEndOfDay(this DateTime dateTime_)
//{
//    DateTime dt = new DateTime(dateTime_.Year, dateTime_.Month, dateTime_.Day, 23, 59, 59, 999);
//    return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dt, "India Standard Time", TimeZoneInfo.Local.Id);
//}

//public string ConvertDateTimeFormat(DateTime timeInEST_)
//{
//    return timeInEST_.ToString("dd MMM yy hh:mm ttt");
//}