using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ExportFileController : BaseController
    {

        private readonly IImpactHistoryBussiness _impactBusiness;
        private readonly ILoginReportBussiness _loginBusiness;
        public ExportFileController(IImpactHistoryBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ILoginReportBussiness loginReportBussiness
            ) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _loginBusiness = loginReportBussiness;
        }

        [AllowAnonymous]
        [HttpGet("~/api/ExportFile/ExportFileCallReport")]
        public async Task<IResult> ExportFileCallReport()
        {
            var dateTimeHandle = DateTime.Now.AddDays(-1);
            var result = await _impactBusiness.ExportFileCallReportMirae(new VS.core.Request.ImpactHistoryExportMiraeCallReqeust()
            {
                From = dateTimeHandle
            });
            return Results.Ok(result);

        }


        [AllowAnonymous]
        [HttpGet("~/api/ExportFile/WokingTime")]
        public async Task<IResult> WokingTime()
        {
            var dateTimeHandle = DateTime.Now.AddDays(-1);
            var result = await _loginBusiness.ExportLogin(
                        new VS.core.Request.LoginReportSerarchRequest()
                        {
                            From = dateTimeHandle,
                            To = new DateTime(dateTimeHandle.Year, dateTimeHandle.Month, dateTimeHandle.Day, 23, 59, 59)
                        });
            return Results.Ok(result);
        }

    }
}