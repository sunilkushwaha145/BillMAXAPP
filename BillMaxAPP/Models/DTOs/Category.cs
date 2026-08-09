using System.ComponentModel.DataAnnotations;

namespace BillMaxAPP.Models
{
    public class Category:BaseColumns
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsSelected { get; set; }


    }
}



