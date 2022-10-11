using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.core.Utilities;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LoginController : BaseController
    {
        public LoginController(IUserBusiness userBusiness) : base(userBusiness)
        {

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
            string token = GetToken(loginUser);
            return Results.Ok(token);
        }


        [HttpPost("~/getProfile")]
        public async Task<IResult> GetProfile()
        {
            var user = GetCurrentUser();
            return Results.Ok(user);
        }
    }
}