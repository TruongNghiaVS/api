﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.core.Utilities;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task<IResult> getAllOverView(ReportImpactRequest request)
        {

            var user = GetCurrentUser();
            if (user.RoleId != "2")
            {
                request.UserId = user.Id;
                request.LineCode = user.LineCode;
            }
            var resultSearch = await _impactBusiness.ReportImpactDashboardOverview(request);
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/getAllImpact")]
        public async Task<IResult> getAll(ReportImpactRequest _input)
        {
            var user = GetCurrentUser();
            if (user.RoleId != "2")
            {
                _input.UserId = user.Id;
                _input.LineCode = user.LineCode;
            }
            var resultSearch = await _impactBusiness.GetAllReportImapact(_input);
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/getAllCDR")]
        public async Task<IResult> getAllReportCDR(ReportCDRequest _input)
        {
            var user = GetCurrentUser();
            if (user.RoleId != "2")
            {
                _input.LineCode = user.LineCode;
            }

            var resultSearch = await _impactBusiness.GetAllReportCDR(_input);
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/getAllRecordingFile")]
        public async Task<IResult> getAllRecordingFile(ReportCDRequest _input)
        {
            var user = GetCurrentUser();
            if (user.RoleId != "2")
            {
                _input.LineCode = user.LineCode;
            }
            var resultSearch = await _impactBusiness.GetAllRecordingFile(_input);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/report/ReportRecordingFile")]
        public async Task<IResult> GetAllReportRecordingFile(ReportCDRequest _input)
        {
            var resultSearch = await _handleReportBussiness.CalTalkingTime();
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/report/TestFile")]
        public async Task<IResult> testFile(ReportCDRequest _input)
        {
            var resultSearch = Utils.GetDurationAudio("");
            var fileRecording = Utils.GetFileRecordingFile();
            return Results.Ok(fileRecording);
        }

    }
}