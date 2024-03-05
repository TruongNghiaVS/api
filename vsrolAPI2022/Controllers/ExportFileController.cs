using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.core.Utilities;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ExportFileController : BaseController
    {

        private readonly IReportBussiness _impactBusiness;

        private IExportFileBussiness _business;

        public ExportFileController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness,
            IExportFileBussiness handleReportBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _business = handleReportBussiness;
        }
        [AllowAnonymous]
        [HttpPost("~/api/exportfile/ExportCase")]
        public async Task<IResult> ExportCase()
        {
            var request = new CampagnProfileExportRequest()
            {
                From = DateTime.Now.AddMonths(-1),
                To = DateTime.Now,
                Limit = 10,
                VendorId = 8,
                CampaignId = "1044"

            };

            var result = _business.HandleExportFile(request);
            return Results.Ok(true);
        }

    }
}