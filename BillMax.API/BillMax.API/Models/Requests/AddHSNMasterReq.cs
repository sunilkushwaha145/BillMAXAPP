using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Requests
{
    public class AddHSNMasterReq
    {
        [Key]
        public int HSNId { get; set; }


        public string HSNCode { get; set; }


        public string Description { get; set; }

        public decimal GSTPercentage { get; set; }

        public bool IsActive { get; set; }
    }
}

