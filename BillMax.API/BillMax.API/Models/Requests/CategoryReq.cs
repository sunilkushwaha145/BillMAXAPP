using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Requests
{
    public class CategoryReq
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
  
        public int ParentCategoryId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
