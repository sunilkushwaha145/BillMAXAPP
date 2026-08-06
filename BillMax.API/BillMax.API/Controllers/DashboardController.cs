using BillMax.API;
using BillMax.API.Models.Tables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BillMaxAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly AppDBContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardController(
        AppDBContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId()
    {
        return int.Parse(
            _httpContextAccessor.HttpContext!
            .User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet("AdminDashboard")]
    [Authorize(Roles = "Admin")]
    public ResJsonOutput AdminDashboard()
    {
        ResJsonOutput result = new ResJsonOutput();

        try
        {
            var adminId = GetUserId();

            var storeUsers = _dbContext.Users
                .Where(x => x.CrBy == adminId && x.RoleId == 2)
                .Select(x => x.UserId)
                .ToList();

            // ===========================
            // Sales Trend (Week)
            // ===========================
            var salesTrend = Enumerable.Range(0, 7)
                .Select(i => DateTime.Today.AddDays(-6 + i))
                .GroupJoin(
                    _dbContext.Invoices.Where(x => storeUsers.Contains(x.CrBy)),
                    date => date,
                    invoice => invoice.Crd.Date,
                    (date, invoices) => new SalesTrend
                    {
                        SaleDate = date,
                        TotalSales = invoices.Sum(x => (decimal?)x.GrandTotal) ?? 0
                    })
                .ToList();

            // ===========================
            // Month Trend
            // ===========================
            var monthTrend = Enumerable.Range(0, 12)
                .Select(i => new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
                .AddMonths(-i))
                .GroupJoin(
                    _dbContext.Invoices.Where(x => storeUsers.Contains(x.CrBy)),
                    month => new { month.Year, month.Month },
                    invoice => new { invoice.Crd.Year, invoice.Crd.Month },
                    (month, invoices) => new SalesTrend
                    {
                        SaleDate = month,
                        TotalSales = invoices.Sum(x => (decimal?)x.GrandTotal) ?? 0
                    })
                .OrderBy(x => x.SaleDate)
                .ToList();

            // ===========================
            // Year Trend
            // ===========================
            var yearTrend = Enumerable.Range(0, 5)
                .Select(i => DateTime.Today.Year - 4 + i)
                .GroupJoin(
                    _dbContext.Invoices.Where(x => storeUsers.Contains(x.CrBy)),
                    year => year,
                    invoice => invoice.Crd.Year,
                    (year, invoices) => new SalesTrend
                    {
                        SaleDate = new DateTime(year, 1, 1),
                        TotalSales = invoices.Sum(x => (decimal?)x.GrandTotal) ?? 0
                    })
                .ToList();

            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var nextMonthStart = monthStart.AddMonths(1);
            var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            var todaySales = _dbContext.Invoices
                .Where(x => storeUsers.Contains(x.CrBy)
                         && x.Crd.Date == DateTime.Today)
                .Sum(x => (decimal?)x.GrandTotal) ?? 0;

            var yesterdaySales = _dbContext.Invoices
                .Where(x => storeUsers.Contains(x.CrBy)
                         && x.Crd.Date == DateTime.Today.AddDays(-1))
                .Sum(x => (decimal?)x.GrandTotal) ?? 0;

            var salesGrowth = yesterdaySales == 0
                ? 0
                : ((todaySales - yesterdaySales) / yesterdaySales) * 100;

            var model = new AdminDashboard
            {
                NewStoresThisMonth = _dbContext.Stores
                    .Where(x => x.CrBy == adminId)
                    .Count(x => x.Crd >= monthStart && x.Crd < nextMonthStart),

                NewProductsThisWeek = _dbContext.Products
                    .Count(x => storeUsers.Contains(x.CrBy)
                             && x.Crd >= startOfWeek),

                TotalStores = _dbContext.Stores
                    .Count(x => x.CrBy == adminId),

                TodayInvoices = _dbContext.Invoices
                    .Count(x => storeUsers.Contains(x.CrBy)
                             && x.Crd.Date == DateTime.Today),

                TotalProducts = _dbContext.Products
                    .Count(x => storeUsers.Contains(x.CrBy)),

                TotalBills = _dbContext.Invoices
                    .Count(x => storeUsers.Contains(x.CrBy)),

                TopStores = _dbContext.Invoices
                    .Where(x => storeUsers.Contains(x.CrBy))
                    .GroupJoin(_dbContext.Users,
                        i => i.CrBy,
                        u => u.UserId,
                        (i, users) => new { i, users })
                    .SelectMany(x => x.users.DefaultIfEmpty(),
                        (x, u) => new { x.i, u })
                    .GroupJoin(_dbContext.Stores,
                        x => x.u.UserName,
                        s => s.Email,
                        (x, stores) => new { x.i, x.u, stores })
                    .SelectMany(x => x.stores.DefaultIfEmpty(),
                        (x, s) => new TopStore
                        {
                            CrBy = x.i.CrBy,
                            UserName = x.u.UserName,
                            StoreName = s != null ? s.StoreName : "Unknown",
                            GrandTotal = x.i.GrandTotal
                        })
                    .GroupBy(x => new
                    {
                        x.CrBy,
                        x.UserName,
                        x.StoreName
                    })
                    .Select(g => new TopStore
                    {
                        CrBy = g.Key.CrBy,
                        UserName = g.Key.UserName,
                        StoreName = g.Key.StoreName,
                        GrandTotal = g.Sum(x => x.GrandTotal)
                    })
                    .OrderByDescending(x => x.GrandTotal)
                    .Take(7)
                    .ToList(),

                TodaySales = todaySales,
                TodaySalesGrowth = salesGrowth,
                SalesTrend = salesTrend,
                MonthTrend = monthTrend,
                YearTrend = yearTrend
            };

            result.Data = model;
            result.Status = new ResStatus
            {
                IsSuccess = true,
                StatusCode = "200",
                Message = "Dashboard loaded successfully."
            };
        }
        catch (Exception ex)
        {
            result.Data = null;
            result.Status = new ResStatus
            {
                IsSuccess = false,
                StatusCode = "ERR001",
                Message = ex.Message
            };
        }

        return result;
    }
}