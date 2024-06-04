using Quartz;
using VS.core.API.Global;

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


            UserContainer.GlobalContainer().ClearData();
            await Task.FromResult(true);
        }
    }
}
