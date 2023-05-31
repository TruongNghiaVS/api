using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        Task<bool> CheckDuplicate(string code);
        Task<PackageReponse> GetALl(PackageRequest request);
        Task<GetCountBYMinMaxReponse> GetCountBYMinMax(GetCountBYMinMaxRequest request);
        Task<PackageReponse> GetALlInfo(PackageRequest request);

    }




}
