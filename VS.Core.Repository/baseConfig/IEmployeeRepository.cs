using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IEmployeeRepository : IGenericRepository<Account>
    {
        Task<EmployeeSearchReponse> GetALl(EmployeeSearchRequest request);
        Task<EmployeeSearchReponse> GetDataForExport(EmployeeSearchRequest request);

        Task<bool> CheckDuplicate(string userName, string phone);

    }




}
