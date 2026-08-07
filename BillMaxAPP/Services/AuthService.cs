using BillMaxAPP.Helpers;
using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApiService _api;

        public AuthService(ApiService api)
        {
            _api = api;
        }

        public async Task<ResJsonOutput?> LoginAsync(LoginRequest request)
        {
            return await _api.PostAsync<ResJsonOutput>(
                ApiRoutes.Login,
                request);
        }
        public Task LogoutAsync()
        {
            SecureStorage.Remove("token");
            return Task.CompletedTask;
        }
    }
}
