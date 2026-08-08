using System.ComponentModel.DataAnnotations;

namespace BillMaxAPP.Models
{
    public class Store :BaseColumns
    {

            [Key]

            public int StoreId { get; set; }
            public string StoreName { get; set; }
           
            public string OwnerName { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public bool IsGSTEnabled { get; set; }
            public int Status { get; set; }
            public bool isDelete { get; set; }
           // public ICollection<Invoices> Invoices { get; set; }

    }
}
