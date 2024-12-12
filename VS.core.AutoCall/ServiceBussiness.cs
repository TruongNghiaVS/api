namespace VS.core.AutoCall
{
    public class ServiceBussiness : IServiceBussiness
    {
        private AutocallApi bussiness { get; set; }

        public ServiceBussiness()
        {
            bussiness = new AutocallApi();
        }
        public async Task<List<StatusItem>> GetStatus()
        {
            var data = await bussiness.GetStatusList();
            return data.Calls.ToList();
        }
        public async Task<bool> CallNumber(string phoneNumber, string sip,
            int noId)
        {
            return await bussiness.MakeCall(phoneNumber, sip, noId);
        }
    }
}
