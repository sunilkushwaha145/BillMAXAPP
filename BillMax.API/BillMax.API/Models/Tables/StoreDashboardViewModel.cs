using System;
using System.Collections.Generic;

namespace BillMax.API.Models.Tables
{
    public class StoreDashboardViewModel
    {
        public string StoreUserName { get; set; }
        public string StoreName { get; set; }
        
        // KPIs
        public decimal TodaySales { get; set; }
        public decimal TodaySalesGrowth { get; set; }
        
        public decimal MonthlySales { get; set; }
        public decimal MonthlySalesGrowth { get; set; }
        
        public int TotalProducts { get; set; }
        
        public int TotalBills { get; set; }
        public int TodayBillsCount { get; set; }

        // Lists
        public List<RecentBillDto> RecentBills { get; set; } = new List<RecentBillDto>();
        public List<LowStockProductDto> LowStockProducts { get; set; } = new List<LowStockProductDto>();
    }

    public class RecentBillDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public int ItemsCount { get; set; }
        public decimal GrandTotal { get; set; }
        public string PayType { get; set; }
        public bool PayStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class LowStockProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
