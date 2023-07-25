using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface ISmsBussiness : IGenericBussine<SmsMessage>
    {
        Task<SmsGetallReponse> GetALl(SmsGetallRequest request);


        Task<SmsGetallReponse> GetAllHandle(SmsGetHandleRequest request);
        Task<int> AddSms2(SmsMessage2 entity);
        Task<int> UpdateSms2(SmsMessage2 entity);
        Task<int> CheckExitsCall(SmsMessage2 entity);

        Task<bool> HandleSms();

    }
}
