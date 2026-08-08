using System.ComponentModel.DataAnnotations;

namespace BillMaxAPP.Models
{

    public class StoreTypes
    {
        [Key]
        //StoreTypeId	StoreTypeName	IsActive	Crd
        public int StoreTypeId { get; set; }
        public string StoreTypeName { get; set; }
        public bool IsActive { get; set; }
        public DateTime Crd { get; set; }
      
    }
}
