using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;
using VS.core.Request;

namespace vsrolAPI2022.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DailyReportController : BaseController
{


    private readonly IDailyReportBussiness _business;

    public DailyReportController(IUserBusiness userBusiness,
        IDailyReportBussiness handleReportBussiness) : base(userBusiness)
    {
    
        _business = handleReportBussiness;
    }

    [AllowAnonymous]
    [HttpGet("~/api/dailyReport/GetFileFinalReport")]
    public async Task<ActionResult> GetFileFinalReport(string? fileName)
    {
        if (string.IsNullOrEmpty(fileName)) 
            fileName = DateTime.Now.ToString("dd.MM.yy") + ".xlsx";
        var pathfolder = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\DaillyReport\\totalReport";
        var path = Path.Combine(pathfolder, fileName);
        if (System.IO.File.Exists(path))
            return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
        return NotFound();
    }
    [AllowAnonymous]
    [HttpGet("~/api/dailyReport/exportFinal")]
    public async Task<IResult> ExportFinal(string? fileName)
    {
        var request = new CampagnProfileExportRequest
        {
            From = DateTime.Now.AddMonths(-1),
            To = DateTime.Now,
            Limit = 10,
            VendorId = 8,
            CampaignId = "1044"
        };
        var result = await _business.ExportFileTotal(request, "totalReport");

        return Results.Ok(result);
    }

    [AllowAnonymous]
    [HttpPost("~/api/dailyReport/exportFile")]
    public async Task<ActionResult> ExportFile(CampagnProfileExportRequest request)
    {
        var userCurrent = GetCurrentUser();

        request.UserId = userCurrent.Id;
        

        var userName = userCurrent.UserName;
        
        var result = await _business.ExportFileExcel(request, userName);

        var filePath = "/api/dailyReport/getFileExport?fileName=" + userName + "/" + result;

        return Ok(filePath);
    }

    [AllowAnonymous]
    [HttpGet("~/api/dailyReport/getFileExport")]
    public async Task<ActionResult> GetFileExport(string? fileName)
    {
        var pathfolder = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\report";
        var path = Path.Combine(pathfolder, fileName);
        if (System.IO.File.Exists(path))
            return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
        return NotFound();
    }
}