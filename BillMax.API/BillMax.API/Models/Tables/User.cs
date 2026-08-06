using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Tables
{
    public class User : BaseColumns
    {
        [Key]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? PassHash { get; set; }
        public int RoleId { get; set; }
        public bool Isblocked { get; set; }
        public int StoreId { get; set; }
        public int? LoginAtm { get; set; }
        public DateTime? BlockedDT { get; set; }


    }

}
