using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Models
{
    public class InvoiceResponse
    {
        public bool Success { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string PayType { get; set; }
        public string PayStatus { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Cgst { get; set; }
        public decimal Sgst { get; set; }
        public decimal GrandTotal { get; set; }
        public List<InvoiceItems> Items { get; set; }
    }

    public class InvoiceItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
