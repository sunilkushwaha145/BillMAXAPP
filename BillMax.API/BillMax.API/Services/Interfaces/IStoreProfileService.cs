using BillMax.API.Models.Requests;

namespace BillMax.API.Services.Interfaces
{
    public interface IStoreProfileService
    {
        StoreProfileReq GetStoreProfile();

        bool AddStoreProfile(StoreProfileReq req);
    }
}
