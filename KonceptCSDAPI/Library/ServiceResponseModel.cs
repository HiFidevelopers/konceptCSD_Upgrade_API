using System;
using System.Data;

namespace KonceptSupportLibrary
{
    public class ServiceResponseModel
    {
        public int response { get; set; }

        public object data { get; set; }

        public string sys_message { get; set; }

        public string response_code { get; set; }

        public static implicit operator ServiceResponseModel(DataTable v)
        {
            throw new NotImplementedException();
        }
    }

    public class ServiceResponseModelRows
    {
        public int response { get; set; }

        public object data { get; set; }

        public object dataRows { get; set; }

        public string sys_message { get; set; }

        public string response_code { get; set; }
    }
}