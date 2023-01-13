using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class DashboardController : BaseController
    {

        private readonly IReportBussiness _impactBusiness;

        public DashboardController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
        }

        [HttpPost("~/api/dashboard/getAllOverView")]
        public async Task<IResult> GetInformationOverviewDashboard()
        {
            var totalRecord = await _impactBusiness.GetReportOverviewAgrree();
            var dataInfo = await _impactBusiness.GetOverViewInfo(new GetOverViewInfoRequest());
            var totalSum = 0;
            var totalSumAnswer = 0;
            var percentConnection = 0.0;
            var totalTalking = 0;
            var dataTimeTalking = await _impactBusiness.GetOverViewTimeTalking(new GetOverViewInfoRequest());
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
                    totalTalking = item.Billsec;
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


        [HttpPost("~/api/dashboard/getOverViewByCall")]
        public async Task<IResult> GetDetailOverview()
        {
            var totalRecord = await _impactBusiness.GetReportOverviewAgrree();
            var dataInfo = await _impactBusiness.GetOverViewInfo(new GetOverViewInfoRequest());

            var dataTimeTalking = await _impactBusiness.GetOverViewTimeTalking(new GetOverViewInfoRequest());
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
                    totalTalking = item.Billsec;

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

    }
}