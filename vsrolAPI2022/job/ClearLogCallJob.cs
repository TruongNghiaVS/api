using Quartz;
using VS.Core.Repository.Model;
using VS.core.Request;
using vsrolAPI2022.Controllers;
using VS.Core.Business.Interface;
using DocumentFormat.OpenXml.Drawing;

namespace VS.core.API.job
{
    public class ClearLogCallJob : IJob
    {
        

        public ClearLogCallJob(
           )
        {
        
        }
        public async Task Execute(IJobExecutionContext context)
        {
            MakeCallController.GlobalLogCall.Clear();
            TrackingCallController.DataMember.Clear();
            await Task.FromResult(true);
        }
    }
}
