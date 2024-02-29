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

        [HttpGet("~/api/job/CalculatingTalktime3")]
        public async Task<ActionResult> CalculatingTalktime3()
        {

            var timerun = new DateTime(2024, 2, 15, 0, 0, 0);
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

        [HttpGet("~/api/job/CalculatingTalktime2")]
        public async Task<ActionResult> CalculatingTalktime2()
        {

            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-60);



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

        [HttpGet("~/api/job/RunCalTimeEndDay")]
        public async Task<ActionResult> RunCalTimeEndDay()
        {
            var timerun = DateTime.UtcNow;
            timerun = timerun.AddHours(-3);
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



        [HttpGet("~/api/job/GroupByDate")]
        public async Task<ActionResult> GroupByDate()
        {
            var timeSelect = DateTime.UtcNow;
            await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
            {
                TimeSelect = timeSelect
            });
            Task.WaitAll();
            return Ok(true);


        }


        [HttpGet("~/api/job/RunSumCampagnOverview")]
        public async Task<ActionResult> RunSumCampagnOverview()
        {
            await _campagnBusiness.UpdateOverViewAllCampagn();

            return Ok(true);

        }


        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            var resultSearch = await _handleReportBussiness.CalTalkingTime();
        }


        private async void OnTimedEventGroupByDay(Object source, System.Timers.ElapsedEventArgs e)
        {

            await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
            {

            });

        }


    }
}