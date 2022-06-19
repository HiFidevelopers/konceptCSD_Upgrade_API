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

		DataTable fetchCustomerDescriptionHistory(CustomerDescriptionHistoryFilterModel modell);

		DataTable insertUpdateCustomerDescriptionHistory(CustomerDescriptionHistoryInsertUpdateModel modell);

		DataTable fetchCustomerRequest(CustomerRequestFilterModel modell);

		DataTable insertUpdateCustomerRequest(CustomerRequestInsertUpdateModel modell);

		DataTable fetchTutorSlotsAvailability(TutorSlotsAvailabilityFilterModel modell);

		DataTable insertWeeklySlotAvailibility(WeeklySlotAvailabilityModel model);

	}
}