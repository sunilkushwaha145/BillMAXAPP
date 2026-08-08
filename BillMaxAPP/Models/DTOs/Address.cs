using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMaxAPP.Models
{
    public class Address : BaseColumns
    {
        [Key]
        public int AddressId { get; set; }

        public int StoreId { get; set; }
        
        public string? Street { get; set; }
        public string? Landmark { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public int? Pincode { get; set; }

        [ForeignKey("StoreId")]
        public  Store? Store { get; set; }
    }
}
