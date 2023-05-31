using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class PackageManagementBusiness : BaseBusiness, IPackageManagementBussiness
    {

        public PackageManagementBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> Add(Package entity)
        {
            return _unitOfWork.PackageRe.Add(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.PackageRe.CheckDuplicate(code);
        }

        public Task Delete(Package entity)
        {
            return _unitOfWork.PackageRe.Delete(entity);
        }

        public Task<PackageReponse> GetALl(PackageRequest request)
        {
            return _unitOfWork.PackageRe.GetALl(request);
        }

        public Task<GetCountBYMinMaxReponse> GetCountBYMinMax(GetCountBYMinMaxRequest request)
        {
            return _unitOfWork.PackageRe.GetCountBYMinMax(request);
        }


        public Task<PackageReponse> GetALlInfo(PackageRequest request)
        {
            return _unitOfWork.PackageRe.GetALlInfo(request);
        }

        public Task<Package> Getbyid(string Id)
        {
            return _unitOfWork.PackageRe.GetById(Id);
        }

        public Task<Package> GetByIdAsync(string id)
        {
            return _unitOfWork.PackageRe.GetById(id);
        }

        public Task<int> UpdateAsyn(Package entity)
        {
            return _unitOfWork.PackageRe.Update(entity);
        }
    }
}
