namespace BillMax.API.Models.Tables
{
    public class DailyReport
    {
        public int Bills { get; set; }
        public decimal Sale { get; set; }
        public decimal GST { get; set; }
        public int Customer { get; set; }
        public List<HourReport> HourList { get; set; }
        public DateTime SelectedDate { get; set; }
    }
    public class HourReport {
        public string Hour { get; set; }
        public int Bills { get; set; }
        public decimal Subtotal { get; set; }
        public decimal GST { get; set; }
        
        public decimal Total { get; set; }

    }
}
