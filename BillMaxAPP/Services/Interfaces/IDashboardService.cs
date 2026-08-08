using BillMaxAPP.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<AdminDashboard?> GetAdminDashboardAsync();
        Task<StoreDashboard?> GetStoreDashboardAsync();
    }
}
