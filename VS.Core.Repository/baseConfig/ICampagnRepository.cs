using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface ICampagnRepository : IGenericRepository<Campagn>
    {
        Task<CampagnRequestReponse> GetALl(CampagnRequest request);
        Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request);
        Task<bool> CheckDuplicate(string code);

    }




}
