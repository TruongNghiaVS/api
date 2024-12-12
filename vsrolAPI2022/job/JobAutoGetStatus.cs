using Quartz;
using VS.core.AutoCall;

namespace VS.core.API.job
{
    public class JobAutoGetStatus : IJob
    {
        private readonly IServiceBussiness _serviceBussiness;
        ModelData _modalData;
        public JobAutoGetStatus(
            IServiceBussiness serviceBussiness)
        {
            _serviceBussiness = serviceBussiness;
            _modalData = ModelData.Instance;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var allResult = await _serviceBussiness.GetStatus();
            _modalData.DataRing = allResult;

        }
    }
}
