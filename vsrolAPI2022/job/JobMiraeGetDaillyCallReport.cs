using Quartz;
using VS.core.Request;
using VS.Core.Business.Interface;

namespace VS.core.API.job
{
    public class JobMiraeGetDaillyCallReport : IJob
    {

        private readonly IImpactHistoryBussiness _business;
        public JobMiraeGetDaillyCallReport(

            IImpactHistoryBussiness loginReportBussiness)
        {

            _business = loginReportBussiness;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var timerun = DateTime.Now;
            if (timerun.DayOfWeek == DayOfWeek.Monday)
            {
                timerun = timerun.AddDays(-2);
            }
            else
            {
                DateTime.Now.AddDays(-1);
            }

            timerun = timerun.Date;
            await _business.ExportFileCallReportMirae(
            new ImpactHistoryExportMiraeCallReqeust()
            {
                From = timerun,
                To = new DateTime(timerun.Year,
                timerun.Month,
                timerun.Day, 23, 59, 00)
            });
            await Task.FromResult(true);
        }
    }
}
