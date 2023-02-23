using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface ILineRepository : IGenericRepository<Line>
    {
        Task<bool> CheckDuplicate(string code);
        Task<LineManagementReponse> GetALl(LineManagementRequest request);
        Task<LineManagementReponse> GetDataForExport(LineManagementRequest request);

    }




}
