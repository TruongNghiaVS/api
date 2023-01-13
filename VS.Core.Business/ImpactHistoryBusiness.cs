
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.Campagn;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class ImpactHistoryBusiness : BaseBusiness, IImpactHistoryBussiness
    {

        public ImpactHistoryBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> AddAsync(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.AddAsync(entity);
        }


        public Task Delete(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.Delete(entity);
        }

        public Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request)
        {
            return _unitOfWork.ImpactRe.GetALl(request);
        }

        public Task<ImpactHistory> Getbyid(string Id)
        {
            return _unitOfWork.ImpactRe.GetByIdAsync(Id);
        }

        public Task<ImpactHistory> GetByIdAsync(string id)
        {
            return _unitOfWork.ImpactRe.GetByIdAsync(id);
        }

        public Task<int> UpdateAsyn(ImpactHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
