namespace BillMaxAPP.Models
{
    public class BaseColumns
    {
        public bool isDelete { get; set; }
        public DateTime Crd { get; set; }
        public int CrBy { get; set; }
        public DateTime? Lmd { get; set; }
        public int? Lmby { get; set; }
    }
}