
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using System.Security.Claims;

namespace BillMax.API.Services
{
    public class StoreService : IStoreService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDBContext _dbContext;
        public StoreService(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }
        public int GetUserId()
        {
            int UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return UserId;
        }
        public bool AddStore(StoreReq req)
        {

            try
            {
                Store store = new Store();
                store.StoreName = req.StoreName;
                store.OwnerName = req.OwnerName;
                store.Email = req.Email;
                store.Mobile = req.Mobile;
                store.Password = req.Password;
                
                    store.IsGSTEnabled = req.IsGSTEnabled;
                
                
                store.isDelete = false;
                store.Crd = DateTime.Now;
                store.CrBy = GetUserId();
                store.Lmby = null;
                store.Lmd = null;
                store.Status = 1;
                _dbContext.Stores.Add(store);
                _dbContext.SaveChanges();

                // User Create
                User user = new User();

                user.UserName = req.Email;
                user.PassHash = req.Password;
                user.RoleId = 2;
                user.StoreId = store.StoreId;   //  StoreId Save
                user.isDelete = false;
                user.Crd = DateTime.Now;
                user.CrBy = GetUserId();
                user.Lmd = null;
                user.Lmby = 0;
                user.Isblocked = false;
                user.LoginAtm = 0;
                user.BlockedDT = null;

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Store Profile Create
                StoreProfiles profile = new StoreProfiles();
                profile.StoreId = store.StoreId;
                profile.StoreName = store.StoreName;
                profile.GSTNumber = "";
                profile.Mobile = store.Mobile;
                profile.Email = store.Email;
                profile.Address = "";
                profile.InvoicePrefix = "INV";
                profile.NextInvoiceNumber = 1;
                profile.FooterMessage = "";
                profile.Logo = null;

                profile.Crd = DateTime.Now;
                profile.CrBy = GetUserId();
                profile.Lmd = null;
                profile.Lmby = null;
                profile.isDelete = false;

                _dbContext.StoreProfiles.Add(profile);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
            return false;
        }
        public List<Store> GetAllStors()
        {
            return _dbContext.Stores
                .Where(x => x.isDelete == false && x.CrBy == GetUserId())
                .ToList();
        }

        //public List<Store> GetAllStors()
        //{
        //    var stores = _dbContext.Stores
        //        .Where(x => x.isDelete == false && x.CrBy == GetUserId())
        //        .ToList();

        //    foreach (var store in stores)
        //    {
        //        var profile = _dbContext.StoreProfiles
        //            .FirstOrDefault(p => p.StoreId == store.StoreId && p.isDelete == false);

        //        bool isComplete = false;

        //        if (profile != null)
        //        {
        //            if (profile.GSTNumber != "" && profile.Address != "")
        //            {
        //                isComplete = true;
        //            }
        //        }

        //        if (isComplete == true)
        //        {
        //            store.Status = 4;   
        //        }
        //        else
        //        {
        //            store.Status = 1;   
        //        }
        //    }

        //    return stores;
        //}

        public Store GetStoreById(int storeid)
        {
            return _dbContext.Stores.FirstOrDefault(x=>x.StoreId==storeid && x.isDelete==false && x.CrBy == GetUserId());
        }

        public bool UpdateStore(Store req)
        {
            bool res=false;

            Store store = _dbContext.Stores.Where(x => x.StoreId == req.StoreId && x.CrBy == GetUserId()).FirstOrDefault();
            store.Lmby = GetUserId();
            store.Lmd = DateTime.Now;
            //store.Status = 1;
            store.StoreName=req.StoreName;
            store.OwnerName = req.OwnerName;
            store.Email=req.Email;
            store.Mobile = req.Mobile;
            store.Password = req.Password;
            store.IsGSTEnabled = req.IsGSTEnabled;
            _dbContext.Stores.Update(store);
            _dbContext.SaveChanges(); 
            res=true;
            return res;
        }

        public bool DeleteStore(int storeId)
        {
            var store = _dbContext.Stores.FirstOrDefault(x => x.StoreId == storeId && x.CrBy == GetUserId());

            if (store == null)
                return false;

            // Soft delete (recommended)
            store.isDelete = true;
            store.Lmd = DateTime.Now;
            store.Lmby =  GetUserId();

            _dbContext.SaveChanges();
            return true;
        }

        public (bool Success, string Message) ChangePassword(ChangePasswordReq req)
        {
            try
            {
                int userId = GetUserId();
                var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId && !u.isDelete );

                if (user == null)
                {
                    return (false, "User not found");
                }

                if (user.PassHash != req.OldPassword) 
                {
                    return (false, "Incorrect old password");
                }

                user.PassHash = req.NewPassword;
                _dbContext.SaveChanges();

                return (true, "Password changed successfully");
            }
            catch (Exception ex)
            {
                return (false, "An error occurred while changing the password");
            }
        }
    }
}
