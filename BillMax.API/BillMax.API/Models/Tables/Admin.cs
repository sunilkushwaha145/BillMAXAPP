using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BillMax.API.Models.Tables
{
    [Table("Admins")]
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(500)]
        public string Bio { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }

        public DateTime Crd { get; set; }
        public int CrBy { get; set; }
        public DateTime? Lmd { get; set; }
        public int? Lmby { get; set; }
        public bool isDelete { get; set; }
    }
}
