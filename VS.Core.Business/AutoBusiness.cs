using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class AutoBusiness : BaseBusiness, IAutoBussiness
    {
         
 
        public AutoBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllProfileByCampangReponse> GetAllCampagn(
            GetAllProfileByCampang request
        )
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }

        public Task<CampagnProfile> GetProfileCall(
          
      )
        {
            return _unitOfWork.CampagnProfileRe.GetProfileCall();
        }


        

        public Task<CampagnProfile> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Run()
        {
           _unitOfWork.a
        }

        public Task<int> UpdateAsyn(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
