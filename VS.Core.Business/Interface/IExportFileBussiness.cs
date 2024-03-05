using VS.core.Request;

namespace VS.Core.Business.Interface
{
    public interface IExportFileBussiness
    {
        Task<bool> HandleExportFile(CampagnProfileExportRequest request);


    }
}
