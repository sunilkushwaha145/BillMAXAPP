
using BillMax.API.Models;
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;

namespace BillMax.API.Services
{
    public class LoginService : ILoginService
    {
        private readonly AppDBContext _dbContext;
        public LoginService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public User? AuthenticateUser(Login lg)
        {
            User? user = _dbContext.Users.FirstOrDefault(u => u.UserName == lg.username && u.PassHash == lg.password);
            return user;
        }

        public List<string> GetRolesByUserId(int RoleId)
        {

          return  _dbContext.UserRoles.Where(x => x.RoleId == RoleId).Select(x => x.RoleName).ToList();
            
        }
    }
}
