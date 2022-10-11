using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IGroupReasonRepository : IGenericRepository<GroupReason>
    {
        Task<bool> CheckDuplicate(string code);
        Task<GroupReasonReponse> GetALl(GroupReasonRequest request);
        Task<GroupReasonReponse> GetDataForExport(GroupReasonRequest request);

    }




}
