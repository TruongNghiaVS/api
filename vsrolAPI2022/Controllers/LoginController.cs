using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.core.Report.Model;
using VS.core.Utilities;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        private ILoginReportBussiness _loginReportBussiness;
        public LoginController(
            IUserBusiness userBusiness,
            ILoginReportBussiness loginReportBussiness)
            : base(userBusiness)
        {
            _loginReportBussiness = loginReportBussiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/user/login")]
        public async Task<IResult> Login(UserInput userInput)
        {
            if (string.IsNullOrEmpty(userInput.UserName) ||
                string.IsNullOrEmpty(userInput.Password))
            {
                return Results.BadRequest("misisng user name or passsword");
            }
            var password = userInput.Password;
            password = Utils.getMD5(password);
            var loginUser = await _userBusiness.Login(userInput.UserName, password);
            if (loginUser == null)
            {
                return Results.Unauthorized();
            }
            var loginreport = new LoginReport();
            loginreport.UserName = userInput.UserName;
            loginreport.Name = userInput.UserName;
            loginreport.Action = "Login";
            loginreport.Note = "";
            loginreport.CreatedBy = loginUser.Id;
            loginreport.CreateAt = DateTime.Now;
            await _loginReportBussiness.Add(loginreport);

            string token = GetToken(loginUser);
            return Results.Ok(token);
        }


        [HttpPost("~/getProfile")]
        public async Task<IResult> GetProfile()
        {
            var user = GetCurrentUser();
            return Results.Ok(user);
        }


        [HttpPost("~/api/user/logout")]
        public async Task<IResult> Logout()
        {
            var user = GetCurrentUser();
            var loginreport = new LoginReport();
            loginreport.UserName = user.UserName;
            loginreport.Name = user.UserName;
            loginreport.Action = "Logout";
            loginreport.Note = "";
            loginreport.CreatedBy = user.Id;
            loginreport.CreateAt = DateTime.Now;
            await _loginReportBussiness.Add(loginreport);
            return Results.Ok("Đăng xuất thành công");
        }


    }
}