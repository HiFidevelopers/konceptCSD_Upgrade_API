using KonceptCSDAPI.Managers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace KonceptCSDAPI.Factory
{
	public class EntityMasterFactory
	{
		public IEntityMasterManager EntityMasterManager(IConfiguration configuration, IHostingEnvironment HostingEnvironment)
		{
			IEntityMasterManager returnvalue = new EntityMasterManager(configuration, HostingEnvironment);

			return returnvalue;
		}
	}
}