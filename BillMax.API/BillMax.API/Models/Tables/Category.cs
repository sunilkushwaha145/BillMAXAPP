using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace BillMax.API.Models.Tables
{
    public class Category:BaseColumns
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

    }
}



