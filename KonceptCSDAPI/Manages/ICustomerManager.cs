using KonceptCSDAPI.Models.Customer;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
	public interface ICustomerManager
	{
		DataTable fetchCustomer(CustomerFilterModel modell);

		DataTable insertCustomer(CustomerInsertModel modell);
	}
}