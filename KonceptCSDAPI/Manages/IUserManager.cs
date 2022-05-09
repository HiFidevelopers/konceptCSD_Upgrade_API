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

		DataTable deleteUser(UserDeleteModel modell);

		DataTable fetchUserGroup(UserGroupFilterModel modell);

		DataTable fetchUserGroupMapping(UserGroupMappingModel modell);

		DataTable insertUpdateUserGroup(UserGroupInsertUpdateModel modell);

		DataTable deleteUserGroup(UserGroupDeleteModel modell);

		DataTable fetchUserSlotsAvailability(UserSlotsAvailabilityFilterModel modell);
	}
}