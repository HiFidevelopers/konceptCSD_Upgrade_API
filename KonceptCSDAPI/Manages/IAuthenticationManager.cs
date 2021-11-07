using KonceptCSDAPI.Models.Authentication;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
    public interface IAuthenticationManager
    {
        DataTable Signin(SiginModel modell);
    }
}