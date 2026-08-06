using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;

namespace BillMax.API.Services.Interfaces
{
    public interface ILoginService
    {
        public User? AuthenticateUser(Login lg);
        List<string> GetRolesByUserId(int userId);
    }
}
