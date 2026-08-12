using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMaxAPP.Models
{
    
    public class InvoiceItems
    {
        public int InvItemId { get; set; }

        [ForeignKey(nameof(Invoices))]
        public int InvoiceId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        public string productName { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }

    }
}
