using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IAutoRepository : IGenericRepository<CampagnProfile>
    {
        Task<bool> HandleDataAutoCall();
       

    }
}
