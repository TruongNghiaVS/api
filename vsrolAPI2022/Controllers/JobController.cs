using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.core.Utilities;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class JobController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        private readonly ICallLogBussiness _callLogBussiness;
        private readonly IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;
        private IHandleReportBussiness _handleReportBussiness;
        public JobController(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICallLogBussiness callLogBussiness,
            IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness,
            IHandleReportBussiness handleReportBussiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _callLogBussiness = callLogBussiness;
            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
            _handleReportBussiness = handleReportBussiness;
        }



        [HttpGet("~/api/job/CalculatingTalktime")]
        public async Task<ActionResult> CalculatingTalktime()
        {
            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-12);
            var resultSearch = await _handleReportBussiness.CalTalkingTime(timerun);
            Task.WaitAll();
            var startTime = timerun;
            var endTime = DateTime.Now.AddDays(1).EndDateTime();
            while (startTime < endTime)
            {
                await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
                {
                    TimeSelect = startTime

                });
                Task.WaitAll();
                startTime = startTime.AddDays(1);
            }
            Task.WaitAll();
            return Ok(true);
        }


        [HttpGet("~/api/job/AutoCalTime")]
        public async Task<ActionResult> AutoCalTime()
        {
            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-20);
            var resultSearch = await _handleReportBussiness.CalTalkingTimeAutoBusiness(timerun);
            Task.WaitAll();

            var startTime = timerun;
            var endTime = DateTime.Now.AddDays(1).EndDateTime();
            while (startTime < endTime)
            {
                await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
                {
                    TimeSelect = startTime

                });
                Task.WaitAll();
                startTime = startTime.AddDays(1);
            }
            Task.WaitAll();
            return Ok(true);
        }

        [HttpGet("~/api/job/CalculatingTalktime3")]
        public async Task<ActionResult> CalculatingTalktime3()
        {

            var timerun = DateTime.Now;
            timerun = timerun.AddDays(-3);
            var resultSearch = await _handleReportBussiness.CalTalkingTime(timerun);
            Task.WaitAll();

            var startTime = timerun;
            var endTime = DateTime.Now.AddDays(1).EndDateTime();
            while (startTime < endTime)
            {
                await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
                {
                    TimeSelect = startTime

                });
                Task.WaitAll();
                startTime = startTime.AddDays(1);
            }
            Task.WaitAll();
            return Ok(true);
        }

        [HttpGet("~/api/job/HanleData")]
        public async Task<ActionResult> HanleData()
        {
            var resultSearch = await _handleReportBussiness.HandleData();

            Task.WaitAll();
            return Ok(true);
        }

        [HttpGet("~/api/job/handleBusiness")]
        public async Task<ActionResult> HandleBusiness()
        {

            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-12);
            var resultSearch = await _handleReportBussiness.CalTalkingTime(timerun);
            Task.WaitAll();

            var startTime = timerun;
            var endTime = DateTime.Now.AddDays(1).EndDateTime();
            while (startTime < endTime)
            {
                await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
                {
                    TimeSelect = startTime

                });
                Task.WaitAll();
                startTime = startTime.AddDays(1);
            }
            Task.WaitAll();
            return Ok(true);
        }



    }
}