using BillMax.API.Models.Requests;

namespace BillMax.API.Services.Interfaces
{
    public interface IAdminProfileService
    {
        AdminProfileReq GetAdminProfile();
        bool UpdateAdminProfile(AdminProfileReq req, string webRootPath);
    }
}
