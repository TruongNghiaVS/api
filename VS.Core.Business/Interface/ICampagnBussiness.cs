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

    }
}
