using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IGroupReasonRepository : IGenericRepository<GroupReason>
    {
        Task<bool> CheckDuplicate(string code);
        Task<GroupReasonReponse> GetALl(GroupReasonRequest request);

        Task<ReasonReponse> getAllStatus(int? vendorId, int? userId);


        Task<GroupReasonReponse> GetDataForExport(GroupReasonRequest request);

    }




}
