using Microsoft.AspNetCore.Http;

namespace BillMax.API.Models.Requests
{
    public class AdminProfileReq
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string Bio { get; set; }

        public string Avatar { get; set; }

        public IFormFile AvatarFile { get; set; }
    }
}
