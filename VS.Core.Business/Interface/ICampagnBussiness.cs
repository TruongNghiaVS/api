using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface ICampagnBussiness : IGenericBussine<Campagn>
    {
        Task<Campagn> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code);
        Task<CampagnRequestReponse> GetALl(CampagnRequest request);
        Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request);
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<Profile> GetProfile(string id);
        Task<int> AddProfile(Profile entity);
        Task<int> UpdateProfile(Profile entity);
        Task DeleteProfile(Profile entity);

        Task<bool> HandleImport(CampanginDataImportRequest request);

    }
}
