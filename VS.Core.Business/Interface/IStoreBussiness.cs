using VS.core.Request;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IStoreBussiness : IGenericBussine<Campagn>
    {
        Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request);
        Task<StoreLookingReponse> GetInfo(string token);
        Task<bool> HandleImport(List<StoreSkipInfo> request, Account userLogin);
        Task<bool> RunLargefile(string pathFile = "");
    }
}
