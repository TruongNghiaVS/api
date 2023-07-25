using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IProfileCampagnRepository : IGenericRepository<CampagnProfile>
    {
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<GetAllProfileByCampangReponse>
        ExportDataByCampaign(GetAllProfileByCampang request);

        Task<int> UpdateSkip(CampagnProfile entity);

        Task<int> UpdateSkipData(CampagnProfile entity);


        Task<List<CampagnProfile>> GetALLAsiggnee(GetAllProfileByCampang request);

        Task<bool> AssignedTask(string profileId, string userId);
        Task<bool> DeleteMutipleCam(List<string> dataDelete, string requestId);

        Task<CampagnProfile> GetByNoAgreement(string profileId, string campanid = null);

        Task<CampagnProfile> GetBYNoAreeMentLasted(string profileId);
        Task<CampagnProfile> GetProfileByNoCMND(string noNational);
        Task<List<CampagnProfile>> GetAllInfoSkipp(string noNational);
        Task<bool> HanldleCase(int? id, bool? resetCase, bool? skipp);

        Task<int> ResetCase(CampagnProfile entity);

        Task<int> ImportUpdate(CampagnProfile entity);
        Task<CampagnProfile> GetProfileByNoCMNDv2(string noNational, string cmnd, string phoneNumber);


    }
}
