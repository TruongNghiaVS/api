namespace VS.Core.Business.Interface
{
    public interface IHandleReportBussiness
    {
        Task<int> CalTalkingTime(DateTime? dateGet = null);

    }
}
