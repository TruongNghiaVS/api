

using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.Interface
{
    public interface IloginReportRepository : IGenericRepository<LoginReport>
    {

        public Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request);

        public Task<List<LoginReportMiraeIndexModel>> ExportLogin(LoginReportSerarchRequest loginRequét);
    }
}
