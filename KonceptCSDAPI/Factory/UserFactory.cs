using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KonceptCSDAPI.Factory
{
	public class UserFactory
	{
		public IUserManager UserManager(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
		{
			IUserManager returnvalue = new UserManager(configuration, HostingEnvironment);

			return returnvalue;
		}
	}
}