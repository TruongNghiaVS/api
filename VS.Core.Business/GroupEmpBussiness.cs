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

        public Task<int> Add(GroupEmployee entity)
        {
            return _unitOfWork.GroupEmployRe.Add(entity);
        }

        public Task<int> AddMemberGroup(GroupMember entity)
        {
            return _unitOfWork.GroupEmployRe.AddMemberGroup(entity);
        }

        public Task<int> Deletemember(int id)
        {
            return _unitOfWork.GroupEmployRe.Deletemember(id);
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
        public Task<EmployeeSearchReponse> getMemberByGroup(MemberGroupByIdRequest request)
        {
            return _unitOfWork.GroupEmployRe.getMemberByGroup(request);
        }
        public Task<GroupEmployeeHaveNotInGroupRequestReponse> GetAllMeberHaveNotGroup(GroupEmployeeHaveNotInGroupRequest request)
        {
            return _unitOfWork.GroupEmployRe.GetAllMeberHaveNotGroup(request);
        }

        public Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request)
        {
            return _unitOfWork.GroupEmployRe.GetAllManager(request);
        }


        public Task<GroupEmployee> Getbyid(string Id)
        {
            return _unitOfWork.GroupEmployRe.GetById(Id);
        }

        public Task<GroupEmployee> GetByIdAsync(string id)
        {
            return _unitOfWork.GroupEmployRe.GetById(id);
        }

        public Task<int> UpdateAsyn(GroupEmployee entity)
        {
            return _unitOfWork.GroupEmployRe.Update(entity);
        }


    }
}
