using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class MasterDataBusiness : BaseBusiness, IMasterDataBussiness
    {

        public MasterDataBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(MasterData entity)
        {
            return _unitOfWork.MasterRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.MasterRe.CheckDuplicate(code);
        }

        public Task Delete(MasterData entity)
        {
            return _unitOfWork.MasterRe.Delete(entity);
        }

        public Task<MasterDataReponse> GetALl(MaterDataRequest request)
        {
            return _unitOfWork.MasterRe.GetALl(request);
        }

        public Task<MasterData> Getbyid(string Id)
        {
            return _unitOfWork.MasterRe.GetByIdAsync(Id);
        }

        public Task<MasterData> GetByIdAsync(string id)
        {
            return _unitOfWork.MasterRe.GetByIdAsync(id);
        }

        public Task<MasterDataReponse> GetDataForExport(MaterDataRequest request)
        {
            return _unitOfWork.MasterRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(MasterData entity)
        {
            return _unitOfWork.MasterRe.UpdateAsyn(entity);
        }
    }
}
