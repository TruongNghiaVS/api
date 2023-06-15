
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
        public Task<int> Add(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.Add(entity);
        }


        public Task Delete(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.Delete(entity);
        }

        public Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request)
        {
            return _unitOfWork.ImpactRe.GetALl(request);
        }

        public Task<ImpactHistoryReponse> GetFinal(ImpactHistorySerarchRequest request)
        {
            return _unitOfWork.ImpactRe.GetFinal(request);
        }



        public Task<ImpactHistory> Getbyid(string Id)
        {
            return _unitOfWork.ImpactRe.GetById(Id);
        }

        public Task<ImpactHistory> GetByIdAsync(string id)
        {
            return _unitOfWork.ImpactRe.GetById(id);
        }

        public Task<int> UpdateAsyn(ImpactHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
