namespace BillMaxAPP.Models
{
    public class BillSummary
    {
        public decimal SubTotal { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; } = 0;
        public decimal Total { get; set; }
        public decimal Discount { get; set; } = 0;
          
    }
}
