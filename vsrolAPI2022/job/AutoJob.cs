using Quartz;
using VS.core.AutoCall;

namespace VS.core.API.job
{
    public class AutoJob : IJob
    {
        private IAutoBussiness _autoBussiness;

        public AutoJob(
            IAutoBussiness autoBussiness)
        {
            _autoBussiness = autoBussiness;

        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _autoBussiness.MakeCall();
        }
    }
}
