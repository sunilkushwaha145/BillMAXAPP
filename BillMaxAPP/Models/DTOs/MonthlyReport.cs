namespace BillMaxAPP.Models
{
    public class MonthlyReport
    {
        public int Bills { get; set; }
        public decimal Sale { get; set; }
        public decimal GST { get; set; }
        

        public decimal HighestSale { get; set; }
        public DateTime? HighestSaleDate { get; set; } = DateTime.Now;
        public List<MonthlyChart> chartlst { get; set; }
        public List<BestSales> BestSaleList { get; set; }




    }
    public class MonthlyChart()
    {
        public int Days { get; set; }
        public int Bills { get; set; }
    }
    public class BestSales()
    {


        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Pic { get; set; }
        public decimal Price { get; set; }

    }
}
