namespace BillMax.API.Models.Tables
{
    public class CartItem:BaseColumns
    {
        public int ProductId { get; set; }
       
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal GSTPercentage { get; set; }

        public int Qty { get; set; }

        public decimal PriceWithGST { get; set; }
       
    }
}
