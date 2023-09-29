using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface IWorkplaceNotedRepository : IGenericRepository<WorkplaceNoted>
    {
        Task<WorkplaceNotedReponse> GetALl(WorkplaceNotedRequest request);

    }




}
