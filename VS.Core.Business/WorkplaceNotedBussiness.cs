using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class WorkplaceNotedBussiness : BaseBusiness, IWorkplaceNotedBussiness
    {

        public WorkplaceNotedBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<WorkplaceNotedReponse> GetALl(WorkplaceNotedRequest request)
        {
            return _unitOfWork.Workplace.GetALl(request);
        }
        public Task<int> Add(WorkplaceNoted entity)
        {
            return _unitOfWork.Workplace.Add(entity);
        }


        public Task<WorkplaceNoted> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(WorkplaceNoted entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(WorkplaceNoted entity)
        {
            throw new NotImplementedException();
        }
    }
}
