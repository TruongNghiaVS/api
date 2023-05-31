using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IPackageManagementBussiness : IGenericBussine<Package>
    {
        Task<Package> Getbyid(string Id);
        Task<PackageReponse> GetALl(PackageRequest request);

        Task<GetCountBYMinMaxReponse> GetCountBYMinMax(GetCountBYMinMaxRequest request);
        Task<PackageReponse> GetALlInfo(PackageRequest request);

    }
}
