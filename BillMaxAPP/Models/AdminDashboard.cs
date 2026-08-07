using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Models
{
    public class AdminDashboard
    {


        public int TotalStores { get; set; }

        public int TotalProducts { get; set; }

        public int TotalBills { get; set; }

        public int NewStoresThisMonth { get; set; }
        public int NewProductsThisWeek { get; set; }
        public int TodayInvoices { get; set; }
        public decimal TodaySalesGrowth { get; set; }

        public decimal TodaySales { get; set; }
        public List<SalesTrend> SalesTrend { get; set; }
        public List<SalesTrend> MonthTrend { get; set; }
        public List<SalesTrend> YearTrend { get; set; }

        public List<TopStore> TopStores { get; set; }

    }


    public class TopStore
    {
        public int CrBy { get; set; }
        public string UserName { get; set; }
        public string StoreName { get; set; }
        public decimal GrandTotal { get; set; }
    }

    public class SalesTrend
    {
        public DateTime SaleDate { get; set; }
        public string SaleRang { get; set; }

        public decimal TotalSales { get; set; }
    }

}
