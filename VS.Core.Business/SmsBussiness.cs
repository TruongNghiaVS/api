using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class SmsBussiness : BaseBusiness, ISmsBussiness
    {

        public SmsBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> Add(SmsMessage entity)
        {
            return _unitOfWork.SmsRe.Add(entity);
        }

        public Task Delete(SmsMessage entity)
        {
            throw new NotImplementedException();
        }

        public Task<SmsMessage> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(SmsMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
