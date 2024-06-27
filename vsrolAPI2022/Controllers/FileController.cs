using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;
namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController
    {
        private readonly IFileBussiness _business;
        public FileController(IFileBussiness autoBussiness)

        {
            _business = autoBussiness;
        }

        [HttpGet("~/api/file/getFileResult")]
        public async Task<IResult> GetFileResult()
        {
            await _business.GetFileResult();
            return Results.Ok(new { success = true });
        }

    }
}
