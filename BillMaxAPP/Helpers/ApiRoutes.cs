using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Helpers
{
    public static class ApiRoutes
    {
        // HTTP (Recommended)
        public const string BaseUrl = "https://api.billmax.store/";
        //public const string BaseUrl = "https://localhost:7288/";

        public const string Login = "api/Auth/login";
        public const string AdminDashboard = "api/Dashboard/AdminDashboard";
        public const string StoreDashboard = "api/Dashboard/StoreDashboard";

    }
}
