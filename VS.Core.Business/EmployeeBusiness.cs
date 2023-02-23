using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class EmployeeBusiness : BaseBusiness, IEmployeeBusiness
    {

        public EmployeeBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> AddAsync(Account entity)
        {
            return _unitOfWork.Employees.AddAsync(entity);
        }

        public Task<Account> GetByIdAsync(string id)
        {
            return _unitOfWork.Employees.GetByIdAsync(id);
        }

        public Task<int> UpdateAsyn(Account entity)
        {
            return _unitOfWork.Employees.UpdateAsyn(entity);
        }

        public Task Delete(Account entity)
        {
            return _unitOfWork.Employees.Delete(entity);
        }

        public async Task<Account> Getbyid(string id)
        {
            return await _unitOfWork.Employees.GetByIdAsync(id);
        }
        public async Task<bool> CheckDuplicate(string userName, string phone)
        {
            return await _unitOfWork.Employees.CheckDuplicate(userName, phone);
        }

        public async Task<bool> UpdatePass(string userName, string pass)
        {
            return await _unitOfWork.Employees.UpdatePass(userName, pass);
        }



        public async Task<EmployeeSearchReponse> GetALl(EmployeeSearchRequest request)
        {
            return await _unitOfWork.Employees.GetALl(request);
        }

        public async Task<EmployeeSearchReponse> GetDataForExport(EmployeeSearchRequest request)
        {
            return await _unitOfWork.Employees.GetDataForExport(request);
        }

        public async Task<Account> GetByLineCode(string lineCode)
        {
            return await _unitOfWork.Employees.GetByLineCode(lineCode);
        }


    }
}
