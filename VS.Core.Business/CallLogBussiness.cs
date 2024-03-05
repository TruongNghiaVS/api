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
        public Task<int> Add(LogCall entity)
        {
            return _unitOfWork.CallRe.Add(entity);
        }
        public Task<int> CountCallBYNoAgree(string noAgree, string phone, string lineCode)
        {
            return _unitOfWork.CallRe.CountCallBYNoAgree(noAgree, phone, lineCode);
        }


     

        public Task Delete(LogCall entity)
        {
            throw new NotImplementedException();
        }

        public Task<LogCall> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(LogCall entity)
        {
            throw new NotImplementedException();
        }
    }
}
