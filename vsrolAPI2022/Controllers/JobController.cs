using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
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

        [HttpPost("~/api/job/runAll")]
        public async Task<ActionResult> RunAll()
        {
            // run job  create job campangin 

            // run job create report time

            var t = new System.Timers.Timer();
            t.Interval = 60000;
            t.Elapsed += OnTimedEvent;
            t.AutoReset = true;

            // Start the timer
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