using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;

namespace BillMax.API.Services.Interfaces
{
    public interface IGSTSettingService
    {
        AddHSNMasterReq AddHSNMaster(AddHSNMasterReq req);
        GSTSetting GetGSTSetting();
        bool SaveGSTSetting(GSTSettingReq request);
        bool EditHSNMaster(AddHSNMasterReq req);
    }
}
