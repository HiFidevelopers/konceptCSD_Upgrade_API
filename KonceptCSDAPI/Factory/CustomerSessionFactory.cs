using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KonceptCSDAPI.Factory
{
	public class CustomerSessionFactory
	{
		public ICustomerSessionManager CustomerSessionManager(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
		{
			ICustomerSessionManager returnvalue = new CustomerSessionManager(configuration, HostingEnvironment);

			return returnvalue;
		}
	}
}