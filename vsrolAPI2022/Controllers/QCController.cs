
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.Core.Business.Interface;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class QCController : BaseController
    {


        private readonly IViewRecordingBussiness _viewRecordingBussiness;


        public QCController(
            IUserBusiness userBusiness,
            IViewRecordingBussiness viewRecordingBussiness) : base(userBusiness)
        {


            _viewRecordingBussiness = viewRecordingBussiness;
        }

        [HttpPost("~/api/qc/getData")]
        public async Task<IResult> getData(GetDashboardQcRequest request)
        {

            var searchRequest = new GetDashboardQcRequest()
            {

                From = request.From,
                To = request.To,
                LineCode = request.LineCode,

            };
            var resultSearch = await _viewRecordingBussiness.GetDataQc(searchRequest);
            return Results.Ok(resultSearch);
        }
    }
}