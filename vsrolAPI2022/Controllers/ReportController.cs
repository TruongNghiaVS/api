using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ReportController : BaseController
    {

        private readonly IReportBussiness _impactBusiness;

        private IHandleReportBussiness _handleReportBussiness;


        public ReportController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness,
            IHandleReportBussiness handleReportBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _handleReportBussiness = handleReportBussiness;
        }

        [HttpPost("~/api/report/getAllOverView")]
        public async Task<IResult> getAllOverView()
        {
            var resultSearch = await _impactBusiness.ReportImpactDashboardOverview();
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/getAllImpact")]
        public async Task<IResult> getAll(ReportImpactRequest _input)
        {
            var resultSearch = await _impactBusiness.GetAllReportImapact(_input);
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/getAllCDR")]
        public async Task<IResult> getAllReportCDR(ReportCDRequest _input)
        {
            var resultSearch = await _impactBusiness.GetAllReportCDR(_input);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/report/ReportRecordingFile")]
        public async Task<IResult> GetAllReportRecordingFile(ReportCDRequest _input)
        {
            var resultSearch = await _handleReportBussiness.CalTalkingTime();
            return Results.Ok(resultSearch);
        }

    }
}