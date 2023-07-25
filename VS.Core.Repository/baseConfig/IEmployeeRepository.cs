using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IEmployeeRepository : IGenericRepository<Account>
    {
        Task<EmployeeSearchReponse> GetALl(EmployeeSearchRequest request);
        Task<EmployeeSearchReponse> GetDataForExport(EmployeeSearchRequest request);

        Task<bool> CheckDuplicate(string userName, string phone);

        Task<bool> UpdatePass(string userName, string pass);


        Task<bool> DeleteEmployee(Account entity);

        Task<Account> GetByLineCode(string lineCode);
    }




}
