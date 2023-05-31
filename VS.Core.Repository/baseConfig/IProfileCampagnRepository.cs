using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IProfileCampagnRepository : IGenericRepository<Profile>
    {
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<int> UpdateSkip(Profile entity);


        Task<List<Profile>> GetALLAsiggnee(GetAllProfileByCampang request);

        Task<bool> AssignedTask(string profileId, string userId);

        Task<Profile> GetByNoAgreement(string profileId, string campanid = null);

        Task<Profile> GetBYNoAreeMentLasted(string profileId);
        Task<Profile> GetProfileByNoCMND(string noNational);
        Task<List<Profile>> GetAllInfoSkipp(string noNational);
        Task<bool> HanldleCase(int? id, bool? resetCase, bool? skipp);

        Task<int> ResetCase(Profile entity);

        Task<int> ImportUpdate(Profile entity);

    }
}
