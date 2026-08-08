using System.ComponentModel.DataAnnotations;

namespace BillMaxAPP.Models
{
    
    public class Customers:BaseColumns
    {
        [Key]
        public int CustId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(12)]
        public string Mobile { get; set; }

        

    }
}
