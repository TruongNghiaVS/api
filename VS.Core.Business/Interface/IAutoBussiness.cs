using VS.core.Request;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;


namespace VS.Core.Business.Interface
{
    public interface IAutoBussiness : IGenericBussine<CampagnProfile>
    {
       
        Task<GetAllProfileByCampangReponse> GetAllCampagn(GetAllProfileByCampang request);
        Task<CampagnProfile> GetProfileCall();
        Task<bool> Run();


    }
}
