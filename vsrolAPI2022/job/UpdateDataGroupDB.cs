
using Newtonsoft.Json;
using Quartz;
using System.Text.RegularExpressions;
using vsrolAPI2022.Controllers;
using System.Net.Http.Headers;
using VS.Core.Business.Interface;
using VS.Core.Repository.Model;
using VS.core.Request;
namespace VS.core.API.job
{
    public class UpdateDataGroupDB : IJob
    {

        private IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;
        public UpdateDataGroupDB(IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness)
        {
            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var request = new GetAllRecordGroupByLineCodeRequest()
            {
                From = DateTime.Now,
                To = DateTime.Now,
                UserId = "1",
                TimeSelect = DateTime.Now
            };
     


            var result =    await _reportTalkTimeGroupByDayBussiness
                .GetAllTracking(request);
            if (result != null)
            {
                TrackingCallController.OutPutData = result.Data as List<TrackingRecordGroupByLineCodeIndexModel>;
            }
            else
            {
                TrackingCallController.OutPutData = new List<TrackingRecordGroupByLineCodeIndexModel>();
            }
            await Task.FromResult(true);
        }
    }
}
