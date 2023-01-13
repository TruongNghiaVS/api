using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IMasterDataNewRepository : IGenericRepository<MasterDataNew>
    {
        Task<bool> CheckDuplicate(string code);
        Task<MasterDataNewReponse> GetALl(MaterDataNewRequest request);
        Task<MasterDataReponse> GetDataForExport(MaterDataRequest request);

    }




}
