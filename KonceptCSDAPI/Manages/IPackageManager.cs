using KonceptCSDAPI.Models.Package;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
	public interface IPackageManager
	{
		DataTable fetchPackage(PackageFilterModel modell);

		DataTable insertUpdatePackage(PackageInsertUpdateModel modell);

		DataTable deletePackage(PackageDeleteModel modell);

	}
}