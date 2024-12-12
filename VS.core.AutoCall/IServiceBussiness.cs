namespace VS.core.AutoCall
{
    public interface IServiceBussiness
    {
        public Task<List<StatusItem>> GetStatus();

        public Task<bool> CallNumber(string phoneNumber, string sip, int noId);

    }
}
