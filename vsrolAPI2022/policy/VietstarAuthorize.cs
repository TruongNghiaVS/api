using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace VS.core.API.policy
{
    public class VietstarAuthorize : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
         

            //context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();

            //return;
        }
    }
}
