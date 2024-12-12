using Quartz;
using VS.core.Request;
using VS.Core.Business.Interface;

namespace VS.core.API.job
{
    public class JobMiraeGetWorkingTimeReport : IJob
    {

        private readonly ILoginReportBussiness _business;
        public JobMiraeGetWorkingTimeReport(

            ILoginReportBussiness loginReportBussiness)
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
                timerun = timerun.AddDays(-1);
            }

            await _business.ExportLogin(new LoginReportSerarchRequest()
            {
                From = timerun.Date,
                To = timerun
            });
            await Task.FromResult(true);
        }
    }
}
