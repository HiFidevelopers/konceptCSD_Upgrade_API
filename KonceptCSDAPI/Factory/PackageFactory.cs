using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KonceptCSDAPI.Factory
{
	public class PackageFactory
	{
		public IPackageManager PackageManager(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
		{
			IPackageManager returnvalue = new PackageManager(configuration, HostingEnvironment);

			return returnvalue;
		}
	}
}