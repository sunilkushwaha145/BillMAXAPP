using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Requests
{
    public class GSTSettingReq
    {
        public int Id { get; set; }

        [Required]
        public decimal Slab1 { get; set; }

        [Required]
        public decimal Slab2 { get; set; }

        [Required]
        public decimal Slab3 { get; set; }

        [Required]
        public decimal Slab4 { get; set; }

        [Required]
        public decimal Slab5 { get; set; }

        [Required]
        public decimal DefaultSlab { get; set; }

        public bool ApplyGST { get; set; }

        public bool ShowCGSTSGST { get; set; }

        public bool InclusiveGST { get; set; }

        public bool RoundOffTotal { get; set; }
    }
}
