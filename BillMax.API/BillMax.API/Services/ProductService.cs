
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace BillMax.API.Services
{

   
    public class ProductService: IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDBContext _dbContext;
        public ProductService(AppDBContext dbContext,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public List<Product> GetAllStors()
        {
            return _dbContext.Products
                .Where(x => (x.isDelete == false || x.isDelete == null) && x.CrBy == GetUserId())
                .ToList();

         
        }

        public bool AddProduct(ProductReq req)
        {
            try
            {
                Product product = new Product();
                product.ProductName = req.ProductName;
                product.ProductCode = req.ProductCode;

                if (_dbContext.Products.Any(x => x.ProductCode == req.ProductCode && x.CrBy == GetUserId()))
                {
                    return false;
                }

                product.Category = req.Category;
                product.Price = req.Price;
                product.Quantity = req.Quantity;
                product.IsGSTApplicable = req.IsGSTApplicable;
                product.GSTPercentage = req.GSTPercentage;
               
                product.Description= req.Description;
                product.ProductImage= req.ProductImage;
                product.isDelete = false;
                product.Crd = DateTime.Now;
                product.CrBy = GetUserId();
                product.Lmby = null;
                product.Lmd = null;
                // product.Status = 1;
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
       
        public Product GetProductById(int ProductId)
        {
              return _dbContext.Products.FirstOrDefault(x => x.ProductId == ProductId && x.CrBy == GetUserId());

        }
        public void UpdateProduct(Product model)
        {
            var data = _dbContext.Products.FirstOrDefault(x => x.ProductId == model.ProductId && x.CrBy == GetUserId());

            if (data != null)
            {
                data.ProductName = model.ProductName;
                data.ProductCode = model.ProductCode;
                
                data.Price = model.Price;
                data.Quantity = model.Quantity;
                data.IsGSTApplicable = model.IsGSTApplicable;
                data.GSTPercentage = model.GSTPercentage;
                data.Description = model.Description;

                //Sirf tab image update karo jab nayi image upload hui ho
                if (!string.IsNullOrEmpty(model.ProductImage))
                {
                    data.ProductImage = model.ProductImage;
                }
                //data.ProductImage = model.ProductImage;

                data.Lmd = DateTime.Now;
                data.Lmby = GetUserId();

                _dbContext.SaveChanges();
            }
        }

       
        public List<Product> GetAllProduct()
        {

            return _dbContext.Products
                             .Where(x => x.isDelete == false && x.CrBy== GetUserId())
                             .ToList();

        }
        public void DeleteProduct(int ProductId)
        {
            var data = _dbContext.Products
                                 .FirstOrDefault(x => x.ProductId == ProductId && x.CrBy == GetUserId());

            if (data != null)
            {
                data.isDelete = true;
                data.Lmd = DateTime.Now;
                data.Lmby = GetUserId();

                _dbContext.SaveChanges();
            }
        }

        public List<SelectListItem> GetAllMainCategory()
        {
            var data = _dbContext.category.ToList();
                                
            var list = new List<SelectListItem>();
            list = data.Where(x => x.ParentCategoryId == null && x.isDelete== false && x.CrBy == GetUserId()).Select(x => new SelectListItem {
            Value=x.CategoryId.ToString(),
            Text=x.CategoryName.ToString()
            }).ToList();
            return list;
            
        }

        public List<SelectListItem> GetSubCategoryByParentId(int ParentCategoryId)
        {
            

            var data= _dbContext.category.ToList();
            var list = new List<SelectListItem>();
            list=data.Where(x => x.ParentCategoryId == ParentCategoryId && x.isDelete== false && x.CrBy == GetUserId())
                .Select(x => new SelectListItem
                {
                    Value = x.CategoryId.ToString(),
                    Text = x.CategoryName
                })
                .ToList();

            return list;
        }

        public List<Product> GetFilterProducts(string catId, string searchText)
        {
            

            List<Product> lst = new List<Product>();
            if(catId=="0")
            {
                if(searchText!=null)
                  lst = _dbContext.Products.Where(x => x.ProductName.Contains(searchText) && x.CrBy == GetUserId()).ToList();
                else
                  lst = _dbContext.Products.Where(x=>x.CrBy == GetUserId()).ToList();

            }
            else
            {
                List<string> catList = _dbContext.category
                    .Where(x => x.ParentCategoryId.ToString() == catId && x.CrBy == GetUserId())
                    .Select(x => x.CategoryId.ToString())
                    .ToList();

                if (searchText != null)
                {
                    lst = _dbContext.Products
                        .Where(x => x.ProductName.Contains(searchText)
                                 && catList.Contains(x.Category) && x.CrBy == GetUserId())
                        .ToList();
                }
                else
                {
                    lst = _dbContext.Products
                        .Where(x => catList.Contains(x.Category) && x.CrBy == GetUserId())
                        .ToList();
                }
            }
            return lst;
        }

        public int GetProductCount()
        {
            return _dbContext.Products.Where(x => x.CrBy == GetUserId()).Count();
        }

        public int GetCartCount()
        {
            return _dbContext.Products.Where(x => x.CrBy == GetUserId()).Count();
        }

        public int GetUserId()
        {
            int UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return UserId;
        }
    }
}
