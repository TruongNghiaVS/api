using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;
namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoController
    {
        private readonly IAutoBussiness _business;
        public AutoController(
            IAutoBussiness autoBussiness
       )
        {
            _business = autoBussiness;
        }
        [HttpGet("~/api/auto/run")]
        public async Task<IResult> HandlerAutoCall()
        {
            await _business.Run();
            return Results.Ok(new { success = true });
        }

        [HttpGet("~/api/auto/handle")]
        public async Task<IResult> HandleAutoBussiness()
        {
            await _business.HandleAutoBussiness();
            return Results.Ok(new { success = true });
        }
    }
}
