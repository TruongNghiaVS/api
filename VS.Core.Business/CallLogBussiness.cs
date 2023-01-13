
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class CallLogBussiness : BaseBusiness, ICallLogBussiness
    {

        public CallLogBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> AddAsync(CallLog entity)
        {
            return _unitOfWork.CallRe.AddAsync(entity);
        }

        public Task Delete(CallLog entity)
        {
            throw new NotImplementedException();
        }

        public Task<CallLogReponse> GetALl(CallLogSerarchRequest request)
        {
            return _unitOfWork.CallRe.GetALl(request);
        }

        public Task<CallLog> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(CallLog entity)
        {
            throw new NotImplementedException();
        }
    }
}
