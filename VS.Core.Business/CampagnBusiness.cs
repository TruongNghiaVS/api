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
        public Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request)
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }


        public Task<int> UpdateProfile(Profile entity)
        {

            return _unitOfWork.CampagnProfileRe.UpdateAsyn(entity);
        }

        public Task<int> AddProfile(Profile entity)
        {
            return _unitOfWork.CampagnProfileRe.AddAsync(entity);
        }

        public Task<Profile> GetProfile(string id)
        {
            return _unitOfWork.CampagnProfileRe.GetByIdAsync(id);
        }
        public Task DeleteProfile(Profile entity)
        {
            return _unitOfWork.CampagnProfileRe.Delete(entity);
        }

        public Task<bool> HandleImport(CampanginDataImportRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
