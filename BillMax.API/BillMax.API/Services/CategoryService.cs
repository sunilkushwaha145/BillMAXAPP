
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using System.Security.Claims;

namespace BillMax.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDBContext _dbContext;
        public CategoryService(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public bool AddCategory(CategoryReq req)
        {
            try
            {
                Category category = new Category();
                category.CategoryName = req.CategoryName;
                category.ParentCategoryId =
                req.ParentCategoryId == 0 ? null : req.ParentCategoryId;
                category.Description = req.Description;
                category.IsActive = req.IsActive;

                category.isDelete = false;
                category.Crd = DateTime.Now;
                category.CrBy = GetUserId();
                category.Lmby = null;
                category.Lmd = null;
                // product.Status = 1;
                _dbContext.category.Add(category);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }

      

        public List<Category> GetAllCategory()
        {
            return _dbContext.category
                             .Where(x => x.isDelete == false && x.CrBy== GetUserId())
                             .ToList();
        }
        public List<Category> GetAllParentCategory()
        {
            return _dbContext.category.Where(x=>x.ParentCategoryId==null && x.CrBy== GetUserId())
                .ToList();
        }

        public Category GetCategoryById(int CategoryId)
        {
            return _dbContext.category
                             .FirstOrDefault(x => x.CategoryId == CategoryId );
        }



        public void UpdateCategory(Category model)
        {
            var data = _dbContext.category
                               .FirstOrDefault(x => x.CategoryId == model.CategoryId && x.CrBy == GetUserId());

            if (data != null)
            {
                data.CategoryName = model.CategoryName;
                data.ParentCategoryId = model.ParentCategoryId;
                data.Description = model.Description;
                data.IsActive = model.IsActive;
                data.Lmby =GetUserId();
                data.Lmd = DateTime.Now; 

                _dbContext.SaveChanges();
            }
        }

       
        public bool DeleteCategory(int id)
        {
            var data = _dbContext.category.FirstOrDefault(x => x.CategoryId == id && x.CrBy == GetUserId());

            if (data != null)
            {
                data.isDelete = true;
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public dynamic GetSubCategory()
        {
            throw new NotImplementedException();
        }

        public List<Category> GetParentCategory()
        {
            return _dbContext.category
                .Where(x => x.ParentCategoryId == null && !x.isDelete && x.CrBy == GetUserId())
                .ToList();
        }
        public int GetUserId()
        {
            int UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return UserId;
        }
    }
}
