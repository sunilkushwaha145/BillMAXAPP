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

        public async Task<ResJsonOutput> CreateBillAsync(CreateInvoiceRequest request)
        {
            // ASSUMPTION: route + request/response shape. See ApiRoutes.GenerateInvoice
            // and CreateInvoiceRequest/ResJsonOutput.
            return await _apiService.PostAsync<ResJsonOutput>(ApiRoutes.GenerateInvoice, request);
        }

        public async Task<ResJsonOutput> GetBillHistoryAsync()
        {
            return await _apiService.GetAsync<ResJsonOutput>(ApiRoutes.BillingHistory);
        }

        public async Task<InvoiceResponse> GetInvoiceByIdAsync(int invoiceId)
        {
            var url = $"{ApiRoutes.GetInvoice}/{invoiceId}";//https://api.billmax.store/api/Billing/invoice/45

            return await _apiService.GetAsync<InvoiceResponse>(url);
        }

        //public async Task<BillCreateResponse?> CreateBillAsync(BillCreateRequest request)
        //{
        //    // ASSUMPTION: route + request/response shape. See ApiRoutes.CreateBill
        //    // and BillCreateRequest/BillCreateResponse.
        //    return await _apiService.PostAsync<BillCreateResponse>(ApiRoutes.CreateBill, request);
        //}
    }
}