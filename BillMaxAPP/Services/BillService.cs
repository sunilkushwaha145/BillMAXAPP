using BillMaxAPP.Helpers;
using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;

namespace BillMaxAPP.Services
{
    public class BillService : IBillService
    {
        private readonly ApiService _apiService;

        public BillService(ApiService apiService)
        {
            _apiService = apiService;
        }

        //public async Task<BillCreateResponse?> CreateBillAsync(BillCreateRequest request)
        //{
        //    // ASSUMPTION: route + request/response shape. See ApiRoutes.CreateBill
        //    // and BillCreateRequest/BillCreateResponse.
        //    return await _apiService.PostAsync<BillCreateResponse>(ApiRoutes.CreateBill, request);
        //}
    }
}