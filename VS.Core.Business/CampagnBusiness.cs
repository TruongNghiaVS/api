using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class CampagnBusiness : BaseBusiness, ICampagnBussiness
    {

        public CampagnBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(Campagn entity)
        {
            return _unitOfWork.CampagnRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.CampagnRe.CheckDuplicate(code);
        }

        public Task Delete(Campagn entity)
        {
            return _unitOfWork.CampagnRe.Delete(entity);
        }

        public Task<CampagnRequestReponse> GetALl(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetALl(request);
        }

        public Task<Campagn> Getbyid(string Id)
        {
            return _unitOfWork.CampagnRe.GetByIdAsync(Id);
        }

        public Task<Campagn> GetByIdAsync(string id)
        {
            return _unitOfWork.CampagnRe.GetByIdAsync(id);
        }

        public Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(Campagn entity)
        {

            return _unitOfWork.CampagnRe.UpdateAsyn(entity);
        }
    }
}
