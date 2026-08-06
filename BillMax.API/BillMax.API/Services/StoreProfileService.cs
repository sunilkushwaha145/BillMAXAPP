
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace BillMax.API.Services
{
    public class StoreProfileService : IStoreProfileService
    {
        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public StoreProfileService(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = dbContext;
        }



        //public StoreProfileService(AppDBContext context)
        //{
        //    _context = context;
        //}

        public StoreProfileReq GetStoreProfile()
        {
            var count = _context.StoreProfiles.Count();

            Console.WriteLine("StoreProfile Count = " + count);

            User user = _context.Users.FirstOrDefault(x => x.UserId == GetUserId());
            Store store = _context.Stores.FirstOrDefault(x => x.Email == user.UserName);

            StoreProfiles data = _context.StoreProfiles.FirstOrDefault(x=> x.StoreId==store.StoreId);


            Console.WriteLine(data.StoreName);


            return new StoreProfileReq
            {
                StoreId = data.StoreId,
                StoreName = data.StoreName,
                GSTNumber = data.GSTNumber,
                Mobile = data.Mobile,
                Email = data.Email,
                Address = data.Address,
                InvoicePrefix = data.InvoicePrefix,
                NextInvoiceNumber = data.NextInvoiceNumber,
                FooterMessage = data.FooterMessage,
                Logo = data.Logo
            };
        }
        public bool AddStoreProfile(StoreProfileReq req)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserId == GetUserId());
            if (user == null)
                return false;
            Store store = _context.Stores.FirstOrDefault(x => x.Email == user.UserName);
            if (store == null)
                return false;
            StoreProfiles data = _context.StoreProfiles
                .FirstOrDefault(x => x.StoreId == store.StoreId);
            if (data == null)
            {
                // Insert
                data = new StoreProfiles();
                data.StoreId = store.StoreId;
                data.StoreName = store.StoreName;
                data.Crd = DateTime.Now;
                _context.StoreProfiles.Add(data);
            }
            //update//
            data.GSTNumber = req.GSTNumber;
            data.Mobile = req.Mobile;
            data.Email = req.Email;
            data.Address = req.Address;
            data.InvoicePrefix = req.InvoicePrefix;
            data.NextInvoiceNumber = req.NextInvoiceNumber;
            data.FooterMessage = req.FooterMessage;
            data.Logo = req.Logo;


            if (data.GSTNumber != "" && data.GSTNumber != null && data.Address != "" && data.Address != null)
            {
                store.Status = 2;
            }
            else
            {
                store.Status = 1;
            }
            _context.Stores.Update(store);

            _context.SaveChanges();
            return true;
        }

       

        public int GetUserId()
        {
            int UserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return UserId;
        }
    }
}