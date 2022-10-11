using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IMasterDataRepository : IGenericRepository<MasterData>
    {
        Task<bool> CheckDuplicate(string code);
        Task<MasterDataReponse> GetALl(MaterDataRequest request);
        Task<MasterDataReponse> GetDataForExport(MaterDataRequest request);

    }




}
