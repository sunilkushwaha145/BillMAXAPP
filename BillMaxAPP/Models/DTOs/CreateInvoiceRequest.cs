using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Models
{
    public class CreateInvoiceRequest
    {
        public int UserId { get; set; }
        public string? Mobile { get; set; }
        public string? CustomerName { get; set; }
        public string? Paytype { get; set; }
        public Invoices? Invoices { get; set; }
        public List<CartItem> CartItem { get; set; } = new();
    }

}
