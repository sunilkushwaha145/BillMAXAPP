using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BillMax.API.Services.Interfaces
{
    public interface IProductService
    {
        bool AddProduct(ProductReq req);

        List<Product> GetAllProduct();

        Product GetProductById(int ProductId);
        void UpdateProduct(Product model);
        void DeleteProduct(int ProductId);
        List<SelectListItem> GetAllMainCategory();
        
        List<SelectListItem> GetSubCategoryByParentId(int parentCategoryId);
        List<Product> GetFilterProducts(string catId, string searchText);
        int GetProductCount();
        int GetCartCount();
    }
}
