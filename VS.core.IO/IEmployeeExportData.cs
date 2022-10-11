using VS.core.IO.model;

namespace VS.core.IO
{
    public interface IEmployeeExportData
    {

        Task<EmployExportReponse> GetALl(EmployExportRequest request);

    }
}
