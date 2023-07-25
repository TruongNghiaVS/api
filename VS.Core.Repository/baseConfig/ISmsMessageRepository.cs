using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface ISmsMessageRepository : IGenericRepository<SmsMessage>
    {
        Task<SmsGetallReponse> GetALl(SmsGetallRequest request);
        Task<ViewRecordingReponse> GetALlReCording(ViewRecordingRequest request);


        Task<SmsGetallReponse> GetAllHandle(SmsGetHandleRequest request);
        Task<int> AddSms2(SmsMessage2 entity);

        Task<int> UpdateSms2(SmsMessage2 entity);

        Task<int> CheckExitsCall(SmsMessage2 entity);

    }
}
