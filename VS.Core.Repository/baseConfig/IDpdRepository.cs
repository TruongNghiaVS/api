using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IDpdRepository : IGenericRepository<Dpd>
    {
        Task<bool> CheckDuplicate(string code);
        Task<DpdReponse> GetALl(DPDRequest request);

    }




}
