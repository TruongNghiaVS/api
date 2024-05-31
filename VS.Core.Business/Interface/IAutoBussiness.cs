using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IAutoBussiness
    {
        Task<GetAllProfileByCampangReponse> GetAllCampagn(GetAllProfileByCampang request);
        Task<CampagnProfile> GetProfileCall();
        Task<bool> Run();
        Task<bool> HandleAutoBussiness();

    }
}
