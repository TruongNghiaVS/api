using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class GroupEmpBussiness : BaseBusiness, IGroupEmpBussiness
    {

        public GroupEmpBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(GroupEmployee entity)
        {
            return _unitOfWork.GroupEmployRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.GroupEmployRe.CheckDuplicate(code);
        }

        public Task Delete(GroupEmployee entity)
        {
            return _unitOfWork.GroupEmployRe.Delete(entity);
        }

        public Task<GroupEmployeeReponse> GetALl(GroupEmployeeRequest request)
        {
            return _unitOfWork.GroupEmployRe.GetALl(request);
        }
        public Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request)
        {
            return _unitOfWork.GroupEmployRe.GetAllManager(request);
        }


        public Task<GroupEmployee> Getbyid(string Id)
        {
            return _unitOfWork.GroupEmployRe.GetByIdAsync(Id);
        }

        public Task<GroupEmployee> GetByIdAsync(string id)
        {
            return _unitOfWork.GroupEmployRe.GetByIdAsync(id);
        }

        public Task<int> UpdateAsyn(GroupEmployee entity)
        {
            return _unitOfWork.GroupEmployRe.UpdateAsyn(entity);
        }


    }
}
