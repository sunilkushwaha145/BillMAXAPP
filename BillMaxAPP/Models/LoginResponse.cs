using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }

    public class LoginData
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
