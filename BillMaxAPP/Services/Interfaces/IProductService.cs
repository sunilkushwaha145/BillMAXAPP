using BillMaxAPP.Models;

namespace BillMaxAPP.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResJsonOutput> GetProductsByCategoryAsync(Dictionary<string, string> queryParams);
        Task<ResJsonOutput> GetProductByID(int productId);
    }
}