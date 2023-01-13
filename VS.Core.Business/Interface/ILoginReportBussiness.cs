using VS.core.Report.Model;
using VS.core.Request;


namespace VS.Core.Business.Interface
{
    public interface ILoginReportBussiness : IGenericBussine<LoginReport>
    {
        Task<LoginReport> Getbyid(string Id);

        Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request);





    }
}
