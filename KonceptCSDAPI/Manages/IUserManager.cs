using KonceptCSDAPI.Models.User;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
	public interface IUserManager
	{
		DataTable fetchUser(UserFilterModel modell);

		DataTable insertUpdateUser(UserInsertUpdateModel modell);

		DataTable fetchUserGroup(UserGroupFilterModel modell);

		DataTable insertUpdateUserGroup(UserGroupInsertUpdateModel modell);
	}
}