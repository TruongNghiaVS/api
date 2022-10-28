using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IProfileCampagnRepository : IGenericRepository<Profile>
    {
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);

        Task<List<Profile>> GetALLAsiggnee(GetAllProfileByCampang request);




    }
}
