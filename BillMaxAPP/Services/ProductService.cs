using BillMaxAPP.Helpers;
using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;

namespace BillMaxAPP.Services
{
    public class ProductService : IProductService
    {
        private readonly ApiService _apiService;

        public ProductService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResJsonOutput> GetProductByID(int productId)
        {
            return await _apiService.GetAsync<ResJsonOutput>($"{ApiRoutes.GetProductById}/{productId}");
        }

        public async Task<ResJsonOutput> GetProductsByCategoryAsync(Dictionary<string, string> queryParams)
        {
            // ASSUMPTION: route + query param name. See ApiRoutes.ProductsByCategory.
            return await _apiService.GetAsync<ResJsonOutput>(ApiRoutes.ProductsByCategory,queryParams);
        }
    }
}