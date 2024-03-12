using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.IO;
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
        [HttpGet("~/api/exportfile/ExportCase")]
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


            var result = await _business.HandleExportFile2(request, "final");

            
            return Results.Ok(true);
        }

        [AllowAnonymous]
        [HttpGet("~/api/dailyReport/GetFileFinalReport")]
        public async Task<ActionResult> GetFileFinalReport(string? fileName)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                fileName = DateTime.Now.ToString("dd.MM.yy") + ".xlsx";
            }
            var pathfolder = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\DaillyReport\\final";
            var path = Path.Combine(pathfolder, fileName);
            if (System.IO.File.Exists(path))
            {
                return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
            }
            return null;
        }

        [AllowAnonymous]
        [HttpGet("~/api/dailyReport/HandleExportFile3")]
        public async Task<IResult> HandleExportFile3(string? fileName)
        {
            var request = new CampagnProfileExportRequest()
            {
                From = DateTime.Now.AddMonths(-1),
                To = DateTime.Now,
                Limit = 10,
                VendorId = 8,
                CampaignId = "1044"

            };


            var result = await _business.HandleExportFile3(request, "final");


            return Results.Ok(result);
        }
        

    }
}