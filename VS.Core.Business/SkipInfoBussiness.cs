using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class SkipInfoBussiness : BaseBusiness, ISkipInfoBussiness
    {

        public SkipInfoBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddSkip(SkipInfo entity)
        {
            return _unitOfWork.SkipRe.AddSkip(entity);
        }

        public Task<List<SkipInfo>> GetALl(SkipInfoSerarchRequest request)
        {
            return _unitOfWork.SkipRe.GetALl(request);
        }
    }
}
