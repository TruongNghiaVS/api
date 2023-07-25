using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Repository.baseConfig
{
    public interface ISkipInfoRepository
    {


        Task<int> AddSkip(SkipInfo entity);
        Task<List<SkipInfo>> GetALl(SkipInfoSerarchRequest request);

    }




}
