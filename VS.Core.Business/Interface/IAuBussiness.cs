using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IAuBussiness : IGenericBussine<CampagnProfile>
    {

        Task<GetAllProfileByCampangReponse> GetAllCampagn(GetAllProfileByCampang request);
        Task<CampagnProfile> GetProfileCall();
        Task<bool> Run();
        Task<bool> HandleAutoBussiness();

    }
}
