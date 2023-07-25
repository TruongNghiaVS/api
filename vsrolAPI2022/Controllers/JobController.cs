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
            var resultSearch = await _handleReportBussiness.CalTalkingTime();
            Task.WaitAll();
            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-30);
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

        //[HttpGet("~/api/job/rebuildReportGroup")]
        //public async Task<ActionResult> rebuildReportGroup()
        //{
        //    var dtFrom = DateTime.Now.AddDays(-5);
        //    var dtTo = DateTime.Now.AddDays(1);

        //    //var resultSearch = await _handleReportBussiness.CalTalkingTime();
        //    Task.WaitAll();
        //    while (dtFrom < dtTo)
        //    {

        //        await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
        //        {
        //            TimeSelect = dtFrom

        //        });
        //        Task.WaitAll();
        //        dtFrom = dtFrom.AddDays(1);
        //    }

        //    Task.WaitAll();
        //    return Ok(true);

        //}

        [HttpGet("~/api/job/RunSumCampagnOverview")]
        public async Task<ActionResult> RunSumCampagnOverview()
        {
            await _campagnBusiness.UpdateOverViewAllCampagn();

            return Ok(true);

        }

        [HttpPost("~/api/job/runAll")]
        public async Task<ActionResult> RunAll()
        {

            var t = new System.Timers.Timer();
            t.Interval = 60000;
            t.Elapsed += OnTimedEvent;
            t.AutoReset = true;

            t.Enabled = true;


            var z = new System.Timers.Timer();
            z.Interval = 80000;
            z.Elapsed += OnTimedEventGroupByDay;
            z.AutoReset = true;

            // Start the timer
            z.Enabled = true;
            // run job create report ime

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