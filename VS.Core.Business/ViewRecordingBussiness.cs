using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class ViewRecordingBussiness : BaseBusiness, IViewRecordingBussiness
    {

        public ViewRecordingBussiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<ViewRecordingReponse> GetALl(ViewRecordingRequest request)
        {
            return _unitOfWork.SmsRe.GetALlReCording(request);
        }

        public Task<GetDashboardQcReponse> GetDataQc(GetDashboardQcRequest request)
        {
            return _unitOfWork.ViewRe.GetDataQc(request);
        }

        public Task<int> Add(ViewRecording entity)
        {
            return _unitOfWork.ViewRe.Add(entity);
        }

        public Task Delete(ViewRecording entity)
        {
            throw new NotImplementedException();
        }

        public Task<ViewRecording> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(ViewRecording entity)
        {
            throw new NotImplementedException();
        }
    }
}
