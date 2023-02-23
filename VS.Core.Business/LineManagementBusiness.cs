using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class LineManagementBusiness : BaseBusiness, ILineManagementBussiness
    {

        public LineManagementBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> AddAsync(Line entity)
        {
            return _unitOfWork.LineRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.LineRe.CheckDuplicate(code);
        }

        public Task Delete(Line entity)
        {
            return _unitOfWork.LineRe.Delete(entity);
        }

        public Task<LineManagementReponse> GetALl(LineManagementRequest request)
        {
            return _unitOfWork.LineRe.GetALl(request);
        }

        public Task<Line> Getbyid(string Id)
        {
            return _unitOfWork.LineRe.GetByIdAsync(Id);
        }

        public Task<Line> GetByIdAsync(string id)
        {
            return _unitOfWork.LineRe.GetByIdAsync(id);
        }

        public Task<LineManagementReponse> GetDataForExport(LineManagementRequest request)
        {
            return _unitOfWork.LineRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(Line entity)
        {
            return _unitOfWork.LineRe.UpdateAsyn(entity);
        }
    }
}
