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

        public async Task<AdminDashboard?> GetAdminDashboardAsync()
        {
            return await _apiService.GetAsync<AdminDashboard>(ApiRoutes.AdminDashboard);
        }
        public async Task<StoreDashboard?> GetStoreDashboardAsync()
        {
            return await _apiService.GetAsync<StoreDashboard>(ApiRoutes.StoreDashboard);
        }
    }
}
