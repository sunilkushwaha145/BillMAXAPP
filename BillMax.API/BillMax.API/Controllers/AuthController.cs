using BillMax.API.Helpers;
using BillMax.API.Models.Requests;
using BillMax.API.Models.Tables;
using BillMax.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BillMax.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ILoginService _loginService;

        public AuthController(JwtService jwtService, ILoginService loginService)
        {
            _jwtService = jwtService;
            _loginService = loginService;
        }

        [HttpPost("login")]
        public ResJsonOutput Login(Login request)
        {

            ResJsonOutput result=new ResJsonOutput();

            try
            {
                User user = _loginService.AuthenticateUser(request);

                if (user != null)
                {
                    List<string> roles = _loginService.GetRolesByUserId(user.RoleId);

                    var token = _jwtService.GenerateToken(user, roles);

                    result.Data = token;
                    result.Status = new ResStatus { IsSuccess = true, StatusCode = "200", Message = "Login successful" };
                }
                else
                {
                    result = new ResJsonOutput();
                    result.Status = new ResStatus { IsSuccess = false, StatusCode = "401", Message = "Invalid credentials" };
                }
            }
            catch (Exception ex)
            {
                result = new ResJsonOutput();
                result.Status = new ResStatus { IsSuccess = false, StatusCode = "ERR001", Message = ex.Message };

            }
            return result;
        }
    }
}
