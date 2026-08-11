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

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; }


    }
}
