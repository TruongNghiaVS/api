using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ProcessingController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        private readonly ICallLogBussiness _callLogBussiness;
        private readonly IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;

        public ProcessingController(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICallLogBussiness callLogBussiness,
            IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _callLogBussiness = callLogBussiness;
            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
        }


        [HttpPost("~/api/call/makeCall")]
        public async Task<IResult> MakeCall(MakeCallProcessRequest inputRequest)
        {
            var userCall = GetCurrentUser();
            if (string.IsNullOrEmpty(inputRequest.PhoneLog) ||
                string.IsNullOrEmpty(inputRequest.NoAgree))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var callLogInsert = new CallLog()
            {
                CreateAt = DateTime.Now,
                CreatedBy = userCall.Id,
                NoAgree = inputRequest.NoAgree,
                PhoneLog = inputRequest.PhoneLog,
                LineCode = userCall.LineCode,
                UpdatedBy = userCall.Id
            };
            var ressult = await _callLogBussiness.AddAsync(callLogInsert);
            var ressultReponse = new
            {
                success = ressult > 0

            };
            return Results.Ok(ressultReponse);

        }



        [HttpPost("~/api/dashboard/Calculating")]
        public async Task<IResult> Calculating()
        {
            //var userLogin = GetCurrentUser();
            var request = new GetReportOverviewAgrreeRequest();
            //request.LineCode = userLogin.LineCode;
            //request.UserId = userLogin.Id;

            await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
            {

            });
            var informationDashboard = new DashboardDetailReponse()
            {
                PercentConnection = 0,
                SumNoAgree = 0,
                SumTimeCall = 0,
                TalkTime = 0
            };
            return Results.Ok(informationDashboard);
        }



    }
}