using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
 
    [Route("[controller]")]

    public class AutoController : BaseController
    {
        private readonly IAutoBussiness _business;
        public AutoController(
            IAutoBussiness autoBussiness,
            IUserBusiness userBusiness
           
        ) : base(userBusiness)
        {
            _business = autoBussiness;
        }
       
        
        [HttpGet("~/api/auto/nextCall")]
        public async Task<IResult> HandlerAutoCall()
        {
            await _business.Run();
            return Results.Ok(new { success = true });
        }
        
        [AllowAnonymous]
        [HttpGet("~/api/auto/handleBusinessCall")]
        public async Task<IResult> HandleBusinessCall()
        {
            await _business.HandleAutoBussiness();
            return Results.Ok(new { success = true });
        }

    }
}
