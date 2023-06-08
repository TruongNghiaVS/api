using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface ICampagnRepository : IGenericRepository<Campagn>
    {
        Task<CampagnRequestReponse> GetALl(CampagnRequest request);

        Task<int> UpdateSkipData(Campagn entity);
        Task<CampangnOverviewByIdReponse> GetOverViewDashboardById(CampangnOverviewByIdRequest request);


        Task<List<Campagn>> GetALlCampang();
        Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request);
        Task<bool> CheckDuplicate(string code);
        Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request);
        Task<GetOverviewCampaignModelByIdReponse> GetOverviewCampagnById(string campaignId);
        Task<bool> UpdateOverView(string campagnId);
        Task<OverViewCampangnReponse> GetAllOverViewRe();

    }




}
