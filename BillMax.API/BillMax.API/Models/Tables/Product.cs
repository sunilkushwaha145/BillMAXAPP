using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Tables
{
    public class Product:BaseColumns
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool IsGSTApplicable { get; set; }
        public decimal GSTPercentage { get; set; }
        public string? Description { get; set; }
        public string? ProductImage { get; set; }

        

    }
}
