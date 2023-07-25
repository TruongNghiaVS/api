using VS.core.Request;
using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface ISkipInfoBussiness
    {
        Task<int> AddSkip(SkipInfo entity);
        Task<List<SkipInfo>> GetALl(SkipInfoSerarchRequest request);

    }
}
