
using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class LoginReportBusiness : BaseBusiness, ILoginReportBussiness
    {
        public LoginReportBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> Add(LoginReport entity)
        {
            return _unitOfWork.LoginRe.Add(entity);
        }


        public Task Delete(LoginReport entity)
        {
            return _unitOfWork.LoginRe.Delete(entity);
        }

        public Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request)
        {
            return _unitOfWork.LoginRe.GetALl(request);
        }


        public Task<LoginReport> Getbyid(string Id)
        {
            return _unitOfWork.LoginRe.GetById(Id);
        }

        public Task<LoginReport> GetByIdAsync(string id)
        {
            return _unitOfWork.LoginRe.GetById(id);
        }
        public Task<int> UpdateAsyn(LoginReport entity)
        {

            return _unitOfWork.LoginRe.Update(entity);
        }

    }
}
