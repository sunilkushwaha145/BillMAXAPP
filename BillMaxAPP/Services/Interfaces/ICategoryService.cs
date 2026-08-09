using BillMaxAPP.Models;

namespace BillMaxAPP.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ResJsonOutput> GetCategoriesAsync();
    }
}