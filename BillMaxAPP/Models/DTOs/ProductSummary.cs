namespace BillMaxAPP.Models
{
    public class ProductSummary
    {
        public int ProductId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; } 
        public decimal Total { get; set; }
        public int Discount { get; set; } = 0;


    }
}

// Sub Total=price*quantity
// CGST=(SubTotal*12)/100
// total= price+cgst+discount

