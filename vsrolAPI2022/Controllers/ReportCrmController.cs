using DocumentFormat.OpenXml.VariantTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business.Interface;
using VS.core.Request;

namespace vsrolAPI2022.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ReportCrmController : BaseController
{


    private readonly IReportCrmBussiness _business;

    public ReportCrmController(IUserBusiness userBusiness,
        IReportCrmBussiness handleReportBussiness) : base(userBusiness)
    {
    
        _business = handleReportBussiness;
    }
 
    [HttpPost("~/api/reportCrm/GetFileReportTalktime")]
    public async Task<ActionResult> GetFileReportTalktime(
            GetAllRecordGroupByLineCodeExportRequest _input
        )
    {
        var userCurrent = GetCurrentUser();
     
        var request2 = new CrmReportRequest()
        {
            From= _input.From,
            To = _input.To,
            UserId = userCurrent.Id, 
            UserName =userCurrent.UserName
            
        };
        var userName = userCurrent.UserName;
        var filepath = "";
        var pathFile = await _business.ExportFileTaltime(request2);

        var objectReturn = new
        {
            pathFile = pathFile,
            success = true
        };
        return Ok(objectReturn);

    }

    [AllowAnonymous]
    [HttpGet("~/api/reportCrm/dowloadFile")]
    public async Task<ActionResult> dowloadFile(
string? pathFile
       )
    {
        if (System.IO.File.Exists(pathFile))
            return File(System.IO.File.OpenRead(pathFile), "application/octet-stream", Path.GetFileName(pathFile));
          return NotFound();
      
    }

    [AllowAnonymous]
    [HttpPost("~/api/reportCrm/GetReportByStatus")]
    public async Task<ActionResult> GetReportByStatus(
             GetAllRecordGroupByLineCodeExportRequest _input
       )
    {
        var userCurrent = GetCurrentUser();
        var request2 = new CrmReportRequest()
        {
            From = _input.From,
            To = _input.To,
            UserId = userCurrent.Id,
            UserName = userCurrent.UserName

        };
        var pathFile = await _business.ExportStatusOverview(request2);

        var objectReturn = new
        {
            pathFile = pathFile,
            success = true
        };
        return Ok(objectReturn);
    }

    [AllowAnonymous]
    [HttpGet("~/api/reportCrm/GetReportStatusDetail")]
    public async Task<ActionResult> GetReportStatusDetail(

       )
    {
        var request2 = new CrmReportRequest()
        {
            From
            = DateTime.Now,
            To = DateTime.Now,
            UserId = "1"
        };

        var userCurrent = GetCurrentUser();
       
        var userName = userCurrent.UserName;
        var filepath = "";
        var pathFile = await _business.ExportImpactStatusDetail(request2);

        if (System.IO.File.Exists(pathFile))
            return File(System.IO.File.OpenRead(pathFile), "application/octet-stream", Path.GetFileName(pathFile));
        return NotFound();
    }
    
    [HttpPost("~/api/reportCrm/GetsumoffTalktime")]
    public async Task<ActionResult> GetsumoffTalktime(
  GetAllRecordGroupByLineCodeExportRequest _input
    )
    {
        var userCurrent = GetCurrentUser();
        var request2 = new CrmReportRequest()
        {
            From
            = _input.From,
            To = _input.To,
            UserName = userCurrent.UserName,
            UserId = userCurrent.Id
        };
        var pathFile = await _business.GetsumoffTalktime(request2);

        var objectReturn = new
        {
            pathFile = pathFile,
            success = true
        };
        return Ok(objectReturn);
    }



}