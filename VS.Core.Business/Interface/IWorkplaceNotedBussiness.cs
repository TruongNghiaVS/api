using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{

    public interface IWorkplaceNotedBussiness : IGenericBussine<WorkplaceNoted>
    {


        Task<WorkplaceNotedReponse> GetALl(WorkplaceNotedRequest request);
    }
}
