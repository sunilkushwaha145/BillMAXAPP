using BillMaxAPP.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMaxAPP.Models
{
   
    public class Invoices:BaseColumns
    {
        public int InvoiceId { get; set; }

        [ForeignKey(nameof(Customers))]
        public int CustId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        //public int StoreId { get; set; }
        public decimal CGST { get; set; }

        public decimal SGST { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotal { get; set; }

        [StringLength(200)]
        public string PayType { get; set; }

        public bool PayStatus { get; set; }

        //Navigation
        public Customers Customers { get; set; }
        //[ForeignKey(nameof(Store))]
        public int? StoreId { get; set; }

        public Store Store { get; set; }
    }
}


