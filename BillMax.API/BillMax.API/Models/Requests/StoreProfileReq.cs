using Microsoft.AspNetCore.Http;

namespace BillMax.API.Models.Requests
{
    public class StoreProfileReq
    {
        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public string GSTNumber { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }
        
        public string Address { get; set; }

        public string InvoicePrefix { get; set; }

        public int NextInvoiceNumber { get; set; }

        public string FooterMessage { get; set; }

        public string? Logo { get; set; }

        public IFormFile? LogoFile { get; set; }
    }
}