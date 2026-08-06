using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;

namespace BillMax.API.Models.Requests
{
    public class StoreReq : BaseColumns
    {
        public string StoreName { get; set; }
        public string OwnerName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsGSTEnabled { get; set; }
        public bool isDelete { get; set; }

    }
}



