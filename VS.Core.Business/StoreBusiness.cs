using Org.BouncyCastle.Asn1.Crmf;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class StoreBusiness : BaseBusiness, IStoreBussiness
    {
        public StoreBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<int> Add(Campagn entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDuplicate(string code)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Campagn entity)
        {
            throw new NotImplementedException();
        }

        

       
        public Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(
      GetAllProfileByCampang request )
        {
            return _unitOfWork.StoreRe.GetALlProfileByCampaign(request);
        }


        public async Task<bool> HandleImport(List<StoreSkipInfo> dataImport, Account userLogin)
        {
            if(dataImport.Count < 1)
            {
                return true ;
            }    
            foreach (var item in dataImport)
            {
                 await _unitOfWork.StoreRe.AddOrUpdateSkip(item);
            }
            return true;
        }

        public async Task<StoreLookingReponse> GetInfo(string noAgree)
        {
            var itemReponse = new StoreLookingReponse();
            itemReponse.Result = await _unitOfWork.StoreRe.GetByNoAgree(noAgree);
            itemReponse.ListSkipNew = await _unitOfWork.StoreRe.GetAllSkip(noAgree);
            itemReponse.listHistory = await _unitOfWork.ImpactRe.GetAllHistoryBYNoAgree(noAgree);
          

            return itemReponse;
        }


     
        public Task<Campagn> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(Campagn entity)
        {
            throw new NotImplementedException();
        }
    }
}
