using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;
namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InputController
    {
        private readonly IAutoBussiness _business;
        public InputController(IAutoBussiness autoBussiness)

        {
            _business = autoBussiness;
        }
        [HttpGet("~/api/input/loadData")]
        public async Task<IResult> LoadData()
        {
            await _business.LoadData();

            return Results.Ok(new { success = true });
        }

    }
}
