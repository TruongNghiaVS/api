
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TrackingController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;

        private readonly ISkipInfoBussiness _skipInfoBussiness;
        public TrackingController(ICampagnBussiness campagnBusiness,
            ISkipInfoBussiness skipInfoBussiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _skipInfoBussiness = skipInfoBussiness;
        }

       
        [HttpPost("~/api/tracking/requestCheck")]
        public async Task<IResult> ResetCase()
        {
            await _campagnBusiness.ResetCase();
            var resultcheck = new
            {
                sucecss = false
            };
            return Results.Ok(resultcheck);
         

        }





    }
}