using VS.core.Request;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface ICampagnBussiness : IGenericBussine<Campagn>
    {
        Task<Campagn> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code);
        Task<CampagnRequestReponse> GetALl(CampagnRequest request);
        Task<CampangnOverviewByIdReponse> GetOverViewDashboardById(CampangnOverviewByIdRequest request);
        Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request);
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<Profile> GetProfile(string id);
        Task<Profile> GetProfileByNoAgree(string noAgree);
        Task<Profile> GetProfileByNoCMND(string noNational);
        Task<int> AddProfile(Profile entity);
        Task<int> UpdateProfile(Profile entity);
        Task DeleteProfile(Profile entity);
        Task<bool> HandleImport(CampanginDataImportRequest request, Account userLogin);
        Task<List<Profile>> GetALLAsiggnee(GetAllProfileByCampang request);
        Task<CampangeProfileInforReponse> GetIno(string id);
        Task<bool> AssignedTask(string profileId, string userId);
        Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request);
        Task<bool> HandleCase(CampaignProfile_caseRequest request);
        Task<bool> UpdateOverViewAllCampagn();
        Task<bool> ResetCase(string campagnCase = "11");

    }
}
