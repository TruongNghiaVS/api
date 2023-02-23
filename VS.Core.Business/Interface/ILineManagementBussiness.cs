using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface ILineManagementBussiness : IGenericBussine<Line>
    {
        Task<Line> Getbyid(string Id);
        Task<bool> CheckDuplicate(string code);
        Task<LineManagementReponse> GetALl(LineManagementRequest request);

        Task<LineManagementReponse> GetDataForExport(LineManagementRequest request);

    }
}
