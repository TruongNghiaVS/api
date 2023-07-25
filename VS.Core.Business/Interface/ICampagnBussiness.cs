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

        Task<GetAllProfileByCampangReponse> ExportDataByCampaign(GetAllProfileByCampang request);
        Task<CampagnProfile> GetProfile(string id);
        Task<CampagnProfile> GetProfileByNoAgree(string noAgree);
        Task<CampagnProfile> GetProfileByNoCMND(string noNational);

        Task<List<CampagnProfile>> GetAllInfoSkipp(string noNational);

        Task<int> AddProfile(CampagnProfile entity);
        Task<int> UpdateProfile(CampagnProfile entity);
        Task<int> UpdateProfileSkip(CampagnProfile entity);
        Task DeleteProfile(CampagnProfile entity);
        Task<bool> HandleImport(CampanginDataImportRequest request, Account userLogin);


        Task<bool> HandleImportSkip(CampanginDataImportRequest request, Account userLogin);
        Task<bool> HandleImportV2(CampanginDataImportRequest request, Account userLogin);

        Task<List<CampagnProfile>> GetALLAsiggnee(GetAllProfileByCampang request);
        Task<CampangeProfileInforReponse> GetIno(string id);
        Task<bool> AssignedTask(string profileId, string userId);
        Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request);
        Task<bool> HandleCase(CampaignProfile_caseRequest request);
        Task<bool> UpdateOverViewAllCampagn();
        Task<bool> ResetCase(string campagnCase = "11");


        Task<bool> DeleteCampagnFile(List<string> DataDelete, string idRequest);
        Task<CampagnProfile> GetProfileByNoCMNDv2(string noNational, string cmnd, string phoneNumber);

    }
}
