
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using System.Security.Claims;

namespace BillMax.API.Services
{
    public class AdminProfileService : IAdminProfileService
    {
        private readonly AppDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminProfileService(AppDBContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public AdminProfileReq GetAdminProfile()
        {
            int userId = GetUserId();

            var admin = _dbContext.Admins
                .FirstOrDefault(a => a.UserId == userId && !a.isDelete);

            if (admin == null)
            {
                var user = _dbContext.Users.Find(userId);
                if (user == null) return null;

                admin = new Admin
                {
                    UserId   = userId,
                    FullName = user.UserName ?? "Admin",
                    Email    = "",
                    Mobile   = "",
                    Bio      = "",
                    Avatar   = "",          
                    Crd      = DateTime.Now,
                    CrBy     = userId,
                    isDelete = false
                };

                _dbContext.Admins.Add(admin);
                _dbContext.SaveChanges();
            }

            var userRecord = _dbContext.Users.Find(userId);
            var role = userRecord != null
                ? _dbContext.UserRoles.Find(userRecord.RoleId)
                : null;

            return new AdminProfileReq
            {
                AdminId  = admin.AdminId,
                FullName = admin.FullName,
                Email    = admin.Email,
                Mobile   = admin.Mobile,
                Bio      = admin.Bio,
                Avatar   = admin.Avatar,   
                Role     = role?.RoleName ?? "N/A"
            };
        }

        public bool UpdateAdminProfile(AdminProfileReq req, string webRootPath)
        {
            try
            {
                int userId = GetUserId();

                var admin = _dbContext.Admins
                    .FirstOrDefault(a => a.UserId == userId && !a.isDelete);

                if (admin == null) return false;

                admin.FullName = req.FullName?.Trim() ?? admin.FullName;
                admin.Email    = req.Email?.Trim()    ?? admin.Email;
                admin.Mobile   = req.Mobile?.Trim()   ?? admin.Mobile;
                admin.Bio      = req.Bio?.Trim()      ?? admin.Bio;
                admin.Lmd      = DateTime.Now;
                admin.Lmby     = userId;

                if (req.AvatarFile != null && req.AvatarFile.Length > 0)
                {
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                    var ext = Path.GetExtension(req.AvatarFile.FileName).ToLowerInvariant();

                    if (allowedExtensions.Contains(ext))
                    {
                        var avatarFolder = Path.Combine(webRootPath, "images", "avatars");
                        if (!Directory.Exists(avatarFolder))
                            Directory.CreateDirectory(avatarFolder);

                        if (!string.IsNullOrEmpty(admin.Avatar) && admin.Avatar.StartsWith("/images/"))
                        {
                            var oldPath = Path.Combine(webRootPath, admin.Avatar.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                            if (File.Exists(oldPath)) File.Delete(oldPath);
                        }

                        var fileName  = $"admin_{userId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                        var filePath  = Path.Combine(avatarFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            req.AvatarFile.CopyTo(stream);
                        }

                        admin.Avatar = $"/images/avatars/{fileName}";
                    }
                }
                else if (string.IsNullOrEmpty(admin.Avatar))
                {
                    admin.Avatar = "";
                }

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AdminProfileService] UpdateAdminProfile Error: {ex.Message}\n{ex.InnerException?.Message}");
                return false;
            }
        }

        public static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";
            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(parts.Take(2).Select(p => p[0])).ToUpper();
        }

        private int GetUserId()
        {
            var val = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(val))
                throw new UnauthorizedAccessException("User not authenticated.");

            return int.Parse(val);
        }
    }
}
