using BillMaxAPP.Helpers;
using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;

namespace BillMaxAPP.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApiService _apiService;

        public CategoryService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResJsonOutput> GetCategoriesAsync()
        {
            // ASSUMPTION: route + response shape. See ApiRoutes.GetMainCategory.
            return await _apiService.GetAsync<ResJsonOutput>(ApiRoutes.GetMainCategory);
        }
    }
}