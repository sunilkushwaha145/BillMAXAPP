using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMaxAPP.Models;

public class Invoices : BaseColumns
{
    public int InvoiceId { get; set; }

    public int CustId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CGST { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SGST { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal GrandTotal { get; set; }

    [StringLength(200)]
    public string? PayType { get; set; }

    public bool PayStatus { get; set; }

    public Customers? Customers { get; set; }

    public int? StoreId { get; set; }

    public Store? Store { get; set; }
    public List<InvoiceItems>? InvoiceItems { get; set; }
}