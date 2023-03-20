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
    public class DashboardController : BaseController
    {

        private readonly IReportBussiness _impactBusiness;

        private readonly IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;

        public DashboardController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness, IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness

            ) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
        }

        [HttpPost("~/api/dashboard/getAllOverView")]
        public async Task<IResult> GetInformationOverviewDashboard()
        {
            var userLogin = GetCurrentUser();
            var request = new GetReportOverviewAgrreeRequest();
            request.LineCode = userLogin.LineCode;
            request.UserId = userLogin.Id;
            var totalRecord = await _impactBusiness.GetReportOverviewAgrree(request);
            var dataInfo = await _impactBusiness.GetOverViewInfo(
                new GetOverViewInfoRequest()
                {
                    LineCode = request.LineCode
                }
             );
            var totalSum = 0;
            var totalSumAnswer = 0;
            var percentConnection = 0.0;
            var totalTalking = 0;
            var dataTimeTalking = await _impactBusiness.GetOverViewTimeTalking(new GetOverViewInfoRequest()
            {
                LineCode = request.LineCode
            });
            foreach (var item in dataInfo.Data)
            {
                if (item.Type == "ANSWERED")
                {
                    totalSumAnswer = item.Total;
                }
                totalSum += item.Total;
            }
            foreach (var item in dataTimeTalking.Data)
            {
                if (item.Disposition == "ANSWERED")
                {
                    totalTalking = item.Duration;
                }
            }
            if (dataInfo != null)
            {
                if (totalSum > 0)
                {
                    percentConnection = (double)totalSumAnswer / (double)totalSum * 100;
                }
            }
            var informationDashboard = new DashboardDetailReponse()
            {
                PercentConnection = Math.Round(percentConnection, 1),
                SumNoAgree = totalRecord,
                SumTimeCall = totalSum,
                TalkTime = totalTalking
            };
            return Results.Ok(informationDashboard);
        }

        [HttpPost("~/api/dashboard/getOverViewByCall2")]
        public async Task<IResult> GetDetailOverview()
        {

            var userLogin = GetCurrentUser();
            var request = new GetReportOverviewAgrreeRequest();
            request.LineCode = userLogin.LineCode;
            request.UserId = userLogin.Id;
            var requestOverviewInfo = new GetOverViewInfoRequest()
            {
                LineCode = request.LineCode,

            };
            var totalRecord = await _impactBusiness.GetReportOverviewAgrree(request);
            var dataInfo = await _impactBusiness.GetOverViewInfo(requestOverviewInfo);
            var dataTimeTalking = await _impactBusiness.GetOverViewTimeTalking(requestOverviewInfo);
            var totalSum = 0;
            var percentConnection = 0.0;
            var totalSumAnswer = 0;
            var totalTalking = 0;
            var totalCallBusy = 0;
            var totalCallFailed = 0;
            var totalCallNoAswer = 0;
            var sumTimeCall = 0;
            var sumTimeWaiting = 0;
            foreach (var item in dataInfo.Data)
            {
                if (item.Type == "ANSWERED")
                {
                    totalSumAnswer = item.Total;
                }
                else if (item.Type == "BUSY")
                {
                    totalCallBusy = item.Total;
                }
                else if (item.Type == "FAILED")
                {
                    totalCallFailed = item.Total;
                }
                else if (item.Type == "NO ANSWER")
                {
                    totalCallNoAswer = item.Total;
                }
                totalSum += item.Total;

            }
            if (dataInfo != null)
            {
                if (totalSum > 0)
                {
                    percentConnection = (double)totalSumAnswer / (double)totalSum * 100;
                }
            }

            foreach (var item in dataTimeTalking.Data)
            {
                if (item.Disposition == "ANSWERED")
                {
                    totalTalking = item.Duration;

                }
                sumTimeCall += item.Billsec;
                sumTimeWaiting += item.Duration;
            }

            var listResult = new List<DashboardDetailItem>();
            listResult.Add(
                new DashboardDetailItem()
                {
                    Author = "",
                    SumTimeWaiting = 0,
                    SumTimeCall = sumTimeCall,
                    PercentConnection = Math.Round(percentConnection, 1),
                    SumAgree = totalRecord,
                    SumCallCancel = totalCallFailed,
                    SumCallBusy = totalCallBusy,
                    SumAnswered = totalSumAnswer,
                    SumCallChanelError = 0,
                    SumCallErrorServer = 0,
                    SumCallNoAswer = totalCallNoAswer,
                    SumNotCall = 0,
                    SumTimeTalking = totalTalking,
                    Sum = totalSum
                }

            );
            return Results.Ok(listResult);
        }


        [HttpPost("~/api/dashboard/getOverView")]
        public async Task<IResult> getOverView(GetOverViewDashboard request)
        {
            var userLogin = GetCurrentUser();

            if (userLogin.RoleId == "1")
            {
                request.LineCode = userLogin.LineCode;
                request.UserId = userLogin.Id;
            }

            int? VendorId = null;

            if (userLogin.RoleId == "4")
            {
                VendorId = int.Parse(userLogin.Id);
            }

            request.VendorId = VendorId;
            if (request.To.HasValue)
            {
                request.To = request.To.ToEndDateTime();
            }

            request.UserId = userLogin.Id;
            var infomationTalKTime = await _reportTalkTimeGroupByDayBussiness.GetOverViewDashBoard(request);


            return Results.Ok(infomationTalKTime);
        }


        [HttpPost("~/api/dashboard/getDetailOverView")]
        public async Task<IResult> getAll(GetAllRecordGroupByLineCodeRequest _input)
        {
            var currentUser = GetCurrentUser();
            if (_input.To.HasValue)
            {
                _input.To = _input.To.EndDateTime();
            }

            if (currentUser.RoleId == "1")
            {
                _input.LineCode = currentUser.LineCode;
                _input.UserId = currentUser.Id;
            }

            int? VendorId = null;

            if (currentUser.RoleId == "4")
            {
                VendorId = int.Parse(currentUser.Id);
            }

            _input.VendorId = VendorId;
            _input.UserId = currentUser.Id;
            var resultSearch = await _reportTalkTimeGroupByDayBussiness.GetAll(_input);
            return Results.Ok(resultSearch);
        }
    }
}