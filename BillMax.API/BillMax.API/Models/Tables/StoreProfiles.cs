using System.ComponentModel.DataAnnotations;

namespace BillMax.API.Models.Tables
{
    public class StoreProfiles : BaseColumns
    {
        [Key]
        public int StoreProfileId { get; set; }

        public int StoreId { get; set; }

        public string? StoreName { get; set; }

        public string? GSTNumber { get; set; }

        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? InvoicePrefix { get; set; }

        public int NextInvoiceNumber { get; set; }

        public string? FooterMessage { get; set; }

        public string? Logo { get; set; }
    }
}