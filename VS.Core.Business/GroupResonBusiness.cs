using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class GroupResonBusiness : BaseBusiness, IGroupResonBussiness
    {

        public GroupResonBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(GroupReason entity)
        {
            return _unitOfWork.GroupRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.GroupRe.CheckDuplicate(code);
        }

        public Task Delete(GroupReason entity)
        {
            return _unitOfWork.GroupRe.Delete(entity);
        }

        public Task<GroupReasonReponse> GetALl(GroupReasonRequest request)
        {
            return _unitOfWork.GroupRe.GetALl(request);
        }

        public Task<GroupReason> Getbyid(string Id)
        {
            return _unitOfWork.GroupRe.GetByIdAsync(Id);
        }

        public Task<GroupReason> GetByIdAsync(string id)
        {
            return _unitOfWork.GroupRe.GetByIdAsync(id);
        }

        public Task<GroupReasonReponse> GetDataForExport(GroupReasonRequest request)
        {
            return _unitOfWork.GroupRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(GroupReason entity)
        {
            return _unitOfWork.GroupRe.UpdateAsyn(entity);
        }
    }
}
