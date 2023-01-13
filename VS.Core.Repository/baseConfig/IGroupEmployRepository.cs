using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.baseConfig
{
    public interface IGroupEmployRepository : IGenericRepository<GroupEmployee>
    {

        Task<bool> CheckDuplicate(string codeCheck);
        Task<GroupEmployeeReponse> GetALl(GroupEmployeeRequest request);

        Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request = null);
    }

}
