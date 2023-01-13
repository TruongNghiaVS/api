

using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Repository.Interface
{
    public interface IloginReportRepository : IGenericRepository<LoginReport>
    {

        public Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request);
    }
}
