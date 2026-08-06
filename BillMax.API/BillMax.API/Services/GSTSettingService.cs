
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BillMax.API.Services
{
    public class GSTSettingService: IGSTSettingService
    {
        private readonly AppDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GSTSettingService(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor; 
        }


        public GSTSetting GetGSTSetting()
        {
            int userId = GetUserId();
            var user = _dbContext.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
                return new GSTSetting();
            int adminId = user.RoleId == 2 ? user.CrBy : user.UserId;

            var gst = _dbContext.GSTSetting.FirstOrDefault(x => x.CrBy == adminId);
            return gst ?? new GSTSetting();
        }
        public AddHSNMasterReq AddHSNMaster(AddHSNMasterReq req)
        {
            try
            {
                HSNMaster hSN = new HSNMaster();
                hSN.HSNCode = req.HSNCode;
                hSN.Description = req.Description;
                hSN.GSTPercentage = req.GSTPercentage;
                hSN.IsActive = req.IsActive;
                
                hSN.Crd = DateTime.Now;

                _dbContext.HSNMaster.Add(hSN);
                _dbContext.SaveChanges();

                req.HSNId = hSN.HSNId;
                return req;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool EditHSNMaster(AddHSNMasterReq req)
        {
            try
            {
                var hSN = _dbContext.HSNMaster.FirstOrDefault(x => x.HSNId == req.HSNId);
                if (hSN != null)
                {
                    hSN.HSNCode = req.HSNCode;
                    hSN.Description = req.Description;
                    hSN.GSTPercentage = req.GSTPercentage;
                    hSN.IsActive = req.IsActive;
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveGSTSetting(GSTSettingReq request)
        {
            try
            {
                
                GSTSetting gst = _dbContext.GSTSetting
                                           .FirstOrDefault(x => x.CrBy == GetUserId());

                if (gst == null)
                {
                    gst = new GSTSetting();

                    gst.CrBy = GetUserId();
                    gst.Crd = DateTime.Now;

                    _dbContext.GSTSetting.Add(gst);
                }
                else
                {
                    gst.Lmd = DateTime.Now;
                    gst.Lmby = GetUserId();
                }

                // Common fields
                gst.Slab1 = request.Slab1;
                gst.Slab2 = request.Slab2;
                gst.Slab3 = request.Slab3;
                gst.Slab4 = request.Slab4;
                gst.Slab5 = request.Slab5;
                gst.DefaultSlab = request.DefaultSlab;
                gst.ApplyGST = request.ApplyGST;
                gst.ShowCGSTSGST = request.ShowCGSTSGST;
                gst.InclusiveGST = request.InclusiveGST;
                gst.RoundOffTotal = request.RoundOffTotal;

                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int GetUserId()
        {
            int UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return UserId;
        }

    }
}
