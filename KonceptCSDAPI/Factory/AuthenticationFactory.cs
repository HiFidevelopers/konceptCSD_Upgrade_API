using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KonceptCSDAPI.Factory
{
    public class AuthenticationFactory
    {
        public IAuthenticationManager AuthenticationManager(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
        {
            IAuthenticationManager returnvalue = new AuthenticationManager(configuration, HostingEnvironment);

            return returnvalue;
        }
    }
}