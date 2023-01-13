using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class MasterDataNewBussiness : BaseBusiness, IMasterDataNewBussiness
    {

        public MasterDataNewBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(MasterDataNew entity)
        {
            return _unitOfWork.MasterNewRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.MasterNewRe.CheckDuplicate(code);
        }

        public Task Delete(MasterDataNew entity)
        {
            return _unitOfWork.MasterNewRe.Delete(entity);
        }

        public Task<MasterDataNewReponse> GetALl(MaterDataNewRequest request)
        {
            return _unitOfWork.MasterNewRe.GetALl(request);
        }

        public Task<MasterDataNew> Getbyid(string Id)
        {
            return _unitOfWork.MasterNewRe.GetByIdAsync(Id);
        }

        public Task<MasterDataNew> GetByIdAsync(string id)
        {
            return _unitOfWork.MasterNewRe.GetByIdAsync(id);
        }

        public Task<int> UpdateAsyn(MasterDataNew entity)
        {
            return _unitOfWork.MasterNewRe.UpdateAsyn(entity);
        }
        public async Task<MasterDataInfoReponse> GetInfo(MaterDataNewRequest request)
        {
            var reponse = new MasterDataInfoReponse();
            var result = await _unitOfWork.MasterNewRe.GetALl(request);
            reponse.ListData = result.Data;
            var allReason = await _unitOfWork.MasterRe.GetALl(new MaterDataRequest()
            {

            });
            reponse.Data = allReason.Data;
            return reponse;


        }

    }
}
