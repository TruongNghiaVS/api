using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class DpdManagementBusiness : BaseBusiness, IDpdManagementBussiness
    {

        public DpdManagementBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> Add(Dpd entity)
        {
            return _unitOfWork.DpdRe.Add(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.DpdRe.CheckDuplicate(code);
        }

        public Task Delete(Dpd entity)
        {
            return _unitOfWork.DpdRe.Delete(entity);
        }

        public Task<DpdReponse> GetALl(DPDRequest request)
        {
            return _unitOfWork.DpdRe.GetALl(request);
        }

        public Task<Dpd> Getbyid(string Id)
        {
            return _unitOfWork.DpdRe.GetById(Id);
        }

        public Task<Dpd> GetByIdAsync(string id)
        {
            return _unitOfWork.DpdRe.GetById(id);
        }


        public Task<int> UpdateAsyn(Dpd entity)
        {
            return _unitOfWork.DpdRe.Update(entity);
        }
    }
}
