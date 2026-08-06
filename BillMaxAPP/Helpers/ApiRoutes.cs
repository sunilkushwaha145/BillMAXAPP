using System;
using System.Collections.Generic;
using System.Text;

namespace BillMaxAPP.Helpers
{
    public static class ApiRoutes
    {
        // HTTP (Recommended)
        public const string BaseUrl = "http://10.0.2.2:5295/";
        //public const string BaseUrl = "https://localhost:7288/";

        public const string Login = "api/Auth/login";
        public const string AdminDashboard = "api/Dashboard/AdminDashboard";

    }
}
