using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IViewRecordingBussiness : IGenericBussine<ViewRecording>
    {

        Task<ViewRecordingReponse> GetALl(ViewRecordingRequest request);
    }
}
