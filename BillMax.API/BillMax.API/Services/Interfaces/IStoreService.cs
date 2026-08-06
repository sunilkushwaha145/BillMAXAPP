using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;

namespace BillMax.API.Services.Interfaces
{
    public interface IStoreService
    {
        bool AddStore(StoreReq req);
        bool DeleteStore(int storeId);
        List<Store> GetAllStors();
        Store GetStoreById(int storeid);
        bool UpdateStore(Store req);
        (bool Success, string Message) ChangePassword(ChangePasswordReq req);
    }
}
