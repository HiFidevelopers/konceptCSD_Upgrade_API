using KonceptCSDAPI.Models.EntityMaster;
using KonceptSupportLibrary;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Threading.Tasks;

namespace KonceptCSDAPI.Managers
{
    public interface IEntityMasterManager
    {
        DataTable fetchentitymaster(EntityMasterModel modell);
    }
}