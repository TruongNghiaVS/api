using Quartz;
using VS.core.API.Global;
using vsrolAPI2022.Controllers;

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

            UserContainer.GlobalContainer().ClearData();
            await Task.FromResult(true);
        }
    }
}
