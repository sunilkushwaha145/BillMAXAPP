using BillMaxAPP.Models;

namespace BillMaxAPP.Services.Interfaces
{
    public interface IBillService
    {
        //Task<BillCreateResponse?> CreateBillAsync(Invoices request);
        Task<ResJsonOutput> CreateBillAsync(CreateInvoiceRequest request);
        Task<ResJsonOutput> GetBillHistoryAsync();
    }
}