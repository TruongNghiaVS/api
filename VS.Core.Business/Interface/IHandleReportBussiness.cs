namespace VS.Core.Business.Interface
{
    public interface IHandleReportBussiness
    {
        Task<int> CalTalkingTime(DateTime? dateGet = null);
        Task<int> CalTalkingTimeAll(DateTime? dateGet = null);
    }
}
