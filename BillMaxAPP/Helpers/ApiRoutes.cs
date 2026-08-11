namespace BillMaxAPP.Services;

public static class ApiRoutes
{

    public const string BaseUrl = "https://api.billmax.store/";
    // =========================
    // AUTH
    // =========================

    public const string Login = "api/Auth/login";


    // =========================
    // BILLING / CART
    // =========================

    public const string CartCount = "api/Billing/cart-count";//remove
    public const string Cart = "api/Billing/cart";//remove
    public const string AddToCart = "api/Billing/cart/add";//remove
    public const string IncreaseCartItem = "api/Billing/cart/increase";//remove
    public const string DecreaseCartItem = "api/Billing/cart/decrease";//remove
    public const string CartSummary = "api/Billing/cart/summary";//remove

    public const string BillingHistory = "api/Billing/history";
    public const string BillingHistoryFilter = "api/Billing/history/filter";

    public const string GenerateInvoice = "api/Billing/invoice/generate";
    public const string GetInvoice = "api/Billing/invoice/{0}";
    public const string PrintInvoice = "api/Billing/invoice/{0}/print";


    // =========================
    // CATEGORY
    // =========================

    public const string AddCategory = "api/Category/addcategory";
    public const string GetAllCategories = "api/Category/getallcategories";
    public const string GetCategoryById = "api/Category/getcategorybyid/{0}";
    public const string UpdateCategory = "api/Category/updatecategory";
    public const string DeleteCategory = "api/Category/deletecategory/{0}";
    public const string GetParentCategories = "api/Category/getparentcategories";


    // =========================
    // DASHBOARD
    // =========================

    public const string AdminDashboard = "api/Dashboard/AdminDashboard";
    public const string StoreDashboard = "api/Dashboard/StoreDashboard";


    // =========================
    // FILE MANAGEMENT
    // =========================

    public const string UploadFile = "api/FileMgnt/upload";


    // =========================
    // PRODUCTS
    // =========================

    public const string GetAllProducts = "api/Products/getallproducts";
    public const string GetProductById = "api/Products/getproductbyid";
    public const string AddProduct = "api/Products/addproducts";
    public const string UpdateProduct = "api/Products/updateproduct";
    public const string DeleteProduct = "api/Products/deleteproduct/{0}";

    public const string GetMainCategory = "api/Products/getmaincategory";
    public const string GetSubCategory = "api/Products/getsubcategory/{0}";

    public const string ProductsByCategory = "api/Products/getfilterproducts";
    public const string ProductCount = "api/Products/getproductcount";


    // =========================
    // REPORTS
    // =========================

    public const string SalesReport = "api/Reports/salesreport";
    public const string StoreReport = "api/Reports/storereport";
    public const string GSTReport = "api/Reports/gstreport";
    public const string ProductReport = "api/Reports/productreport";

    public const string DailyReport = "api/Reports/dailyreport";
    public const string MonthlyReport = "api/Reports/monthlyreport";


    // =========================
    // SETTINGS
    // =========================

    public const string GetStoreProfile = "api/Settings/getstoreprofile";
    public const string AddStoreProfile = "api/Settings/AddStoreProfile";

    public const string ChangePassword = "api/Settings/changepassword";

    public const string GetInvoiceSettings = "api/Settings/getinvoicesettings";
    public const string SaveInvoiceSettings = "api/Settings/saveinvoicesettings";

    public const string GetGSTSetting = "api/Settings/getgstsetting";
    public const string GetAllHSNMaster = "api/Settings/getallhsnmaster";
    public const string SaveGSTSetting = "api/Settings/savegstsetting";

    public const string GetAddressByPincode =
        "api/Settings/getaddressbypincode/{0}";


    // =========================
    // STORE MANAGEMENT
    // =========================

    public const string GetStoreTypes =
        "api/StoreManagement/getstoretypes";

    public const string GetAllStores =
        "api/StoreManagement/getallstors";

    public const string AddStore =
        "api/StoreManagement/addstore";

    public const string GetStoreById =
        "api/StoreManagement/getstorebyid/{0}";

    public const string UpdateStore =
        "api/StoreManagement/updatestore";

    public const string DeleteStore =
        "api/StoreManagement/deletestore/{0}";


    // =========================
    // DEFAULT WEATHER API
    // =========================

    public const string WeatherForecast = "WeatherForecast";
}