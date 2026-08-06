using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMax.API.Models.Tables
{
    public class GSTSetting: BaseColumns
    {
            
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Slab1 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Slab2 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Slab3 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Slab4 { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal Slab5 { get; set; }

        [Column("DefaultSlab", TypeName = "decimal(5,2)")]
        public decimal DefaultSlab { get; set; }

        public bool ApplyGST { get; set; }
        public bool ShowCGSTSGST { get; set; }
        public bool InclusiveGST { get; set; }
        public bool RoundOffTotal { get; set; }

        [NotMapped]
        public List<HSNMaster> HSNMasterList { get; set; } = new();
    }







    public class HSNMaster
    {
        [Key]
        public int HSNId { get; set; }

   
        public string HSNCode { get; set; }

       
        public string Description { get; set; }

        public decimal GSTPercentage { get; set; }

        public bool IsActive { get; set; }
        
        public DateTime Crd { get;  set; }
    }

    }



