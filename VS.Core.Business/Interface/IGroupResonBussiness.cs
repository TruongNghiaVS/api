using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IGroupResonBussiness : IGenericBussine<GroupReason>
    {
        Task<GroupReason> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code);
        Task<GroupReasonReponse> GetALl(GroupReasonRequest request);

        Task<ReasonReponse> getAllStatus(int? vendorId, int? userId);



        Task<GroupReasonReponse> GetDataForExport(GroupReasonRequest request);

    }
}
