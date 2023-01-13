using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VS.core.API.Resource;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;

namespace vsrolAPI2022.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class BaseController : ControllerBase
    {

        protected readonly IUserBusiness _userBusiness;
        protected readonly MessageInfomation _message;
        public BaseController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
            _message = MessageInfomation.GetInstance();
        }


        protected string GetToken(Account request)
        {
            var issuer = "https://vietstargroup.vn/";
            var audience = "nguyentruongnghia";
            var key = Encoding.ASCII.GetBytes
            ("product of vietstargroup");
            var userName = request.UserName;
            var email = request.UserName;
            var idRequest = request.Id;
            var roleId = request.RoleId;
            var fullName = request.FullName;
            var lineCode = request.LineCode;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("userId", idRequest),
                        new Claim("Name",fullName ),
                        new Claim("RoleUser",roleId ),
                        new Claim("userName", userName),
                        new Claim("email", email),
                        new Claim("lineCode", lineCode)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;

        }
        [Authorize]
        protected Account? GetCurrentUser()
        {
            var identity = this.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                var idUser = identity.Claims.FirstOrDefault(o => o.Type == "userId")?.Value;
                var userName = userClaims.FirstOrDefault(o => o.Type == "userName")?.Value;
                var roleId = userClaims.FirstOrDefault(o => o.Type == "RoleUser")?.Value;
                var fullName = userClaims.FirstOrDefault(o => o.Type == "Name")?.Value;
                var lineCode = userClaims.FirstOrDefault(o => o.Type == "lineCode")?.Value;
                return new Account
                {
                    UserName = userName,
                    Id = idUser,
                    RoleId = roleId,
                    FullName = fullName,
                    LineCode = lineCode
                };
            }
            return null;
        }


    }
}