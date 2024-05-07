using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using VS.core.API.Global;

namespace VS.core.API.policy
{
    public class VietstarAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            var info = context.HttpContext.User.Identity as ClaimsIdentity;
            var idUser = info.Claims.FirstOrDefault(o => o.Type == "userName")?.Value;
            var isValid = UserContainer.GlobalContainer().ValidAcess(idUser);
            if (isValid == false)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;

            }


        }
    }
}
