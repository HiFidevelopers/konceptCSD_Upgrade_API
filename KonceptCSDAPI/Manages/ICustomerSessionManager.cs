using KonceptCSDAPI.Models.CustomerSession;
using KonceptSupportLibrary;
using System.Data;

namespace KonceptCSDAPI.Managers
{
	public interface ICustomerSessionManager
	{
		DataTable fetchCustomerDueSession(CustomerDueSessionFilterModel modell);

		DataTable insertUpdateCustomerRemarks(CustomerRemarksInsertUpdateModel modell);

		DataTable fetchCustomerRemarks(CustomerRemarksFilterModel modell);

	}
}