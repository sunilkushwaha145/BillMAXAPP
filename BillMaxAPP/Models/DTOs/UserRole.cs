using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BillMaxAPP.Models
{
    public class UserRole : BaseColumns
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
    }
}