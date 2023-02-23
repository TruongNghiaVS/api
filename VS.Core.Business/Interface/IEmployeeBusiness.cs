
using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IEmployeeBusiness : IGenericBussine<Account>
    {
        Task<Account> Getbyid(string Id);

        Task<bool> CheckDuplicate(string userName, string phone);

        Task<bool> UpdatePass(string userName, string pass);
        Task<EmployeeSearchReponse> GetALl(EmployeeSearchRequest request);

        Task<EmployeeSearchReponse> GetDataForExport(EmployeeSearchRequest request);

        Task<Account> GetByLineCode(string lineCode);
    }
}
