using Quartz;
using VS.Core.Repository.Model;
using VS.core.Request;
using vsrolAPI2022.Controllers;
using VS.Core.Business.Interface;
using DocumentFormat.OpenXml.Drawing;


namespace VS.core.API.job
{
    public class CalTimeJobCheck : IJob
    {
        private IHandleReportBussiness _handleReportBussiness;
   
        private readonly IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;

        public CalTimeJobCheck(
            IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness,
            IHandleReportBussiness handleReportBussiness
            )
        {
            _handleReportBussiness = handleReportBussiness;
            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
       
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var timerun = DateTime.Now;
            timerun = timerun.AddMinutes(-220);
            await _handleReportBussiness.CalTalkingTime(timerun);
            await _reportTalkTimeGroupByDayBussiness.ProcessCalReportGroupByDay(new GetAllRecordGroupByLineCodeRequest()
            {
                TimeSelect = timerun

            });
          
            await Task.FromResult(true);
        }
    }
}
