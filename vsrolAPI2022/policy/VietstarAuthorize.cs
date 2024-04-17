using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using VS.core.API.Global;
using static Dapper.SqlMapper;

namespace VS.core.API.policy
{
    public class VietstarAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
          
            var info = context.HttpContext.User.Identity as ClaimsIdentity;
            var idUser = info.Claims.FirstOrDefault(o => o.Type == "userName")?.Value;
            var isValid = UserContainer.GlobalContainer().ValidAcess(idUser);
            if(isValid == false )
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;

            }

           
        }
    }
}
