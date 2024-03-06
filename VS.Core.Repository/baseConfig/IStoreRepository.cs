using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;


namespace VS.Core.Repository.baseConfig
{
    public interface IStoreRepository : IGenericRepository<CampagnProfile>
    {
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<GetAllStoreSkipReponse> GetAllSkip(string  noAgree);
        Task<bool> AddOrUpdate(CampagnProfile item);

        Task<bool> AddOrUpdateSkip(StoreSkipInfo item);
        Task<StoreProfile> GetByNoAgree(string noAgree);
    }




}
