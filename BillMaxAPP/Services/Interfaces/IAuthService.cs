using BillMaxAPP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResJsonOutput?> LoginAsync(LoginRequest request);
        Task LogoutAsync();
    }
}
