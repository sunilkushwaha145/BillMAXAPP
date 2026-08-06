using BillMaxAPP.Helpers;
using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApiService _apiService;

        public DashboardService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResJsonOutput?> GetAdminDashboardAsync()
        {
            return await _apiService.GetAsync<ResJsonOutput>(ApiRoutes.AdminDashboard);
        }
    }
}
