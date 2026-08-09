using System.ComponentModel.DataAnnotations;

namespace BillMaxAPP.Models
{
    public class Product:BaseColumns
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int? Category { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public bool IsGSTApplicable { get; set; }
        public decimal GSTPercentage { get; set; }
        public string? Description { get; set; }
        public string? ProductImage { get; set; }

        

    }
}
