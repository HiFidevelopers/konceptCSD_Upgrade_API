using KonceptCSDAPI.Models.CustomerSession;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
	public interface ICustomerSessionManager
	{
		DataTable fetchCustomerDueSession(CustomerDueSessionFilterModel modell);

		DataTable insertUpdateCustomerRemarks(CustomerRemarksInsertUpdateModel modell);

	}
}