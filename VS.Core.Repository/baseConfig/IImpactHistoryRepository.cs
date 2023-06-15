using VS.core.Request;
using VS.Core.dataEntry.Campagn;

namespace VS.Core.Repository.baseConfig
{
    public interface IImpactHistoryRepository : IGenericRepository<ImpactHistory>
    {
        Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request);
        Task<ImpactHistoryReponse> GetFinal(ImpactHistorySerarchRequest request);
        Task<int> AddHistoryImpact(ImpactHistory entity);
        Task<List<ImpactHistory>> GetAllHistory(ImpactHistorySerarchRequest request);

    }




}
