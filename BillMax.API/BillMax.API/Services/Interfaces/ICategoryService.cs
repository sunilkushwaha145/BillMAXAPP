using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;

namespace BillMax.API.Services.Interfaces
{
    public interface ICategoryService
    {
        public bool AddCategory(CategoryReq req);
        bool DeleteCategory(int CategoryId);
        List<Category> GetAllCategory();
        List<Category> GetAllParentCategory();
        

        Category GetCategoryById(int CategoryId);
        List<Category> GetParentCategory();
        dynamic GetSubCategory();
        void UpdateCategory(Category category);
    }
}
