using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class BaseBusiness
    {
        protected readonly IUnitOfWork _unitOfWork;
        public BaseBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




    }
}
