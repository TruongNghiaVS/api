using VS.core.Request;

namespace VS.Core.Business.Interface
{
    public interface IExportFileBussiness
    {
        Task<bool> HandleExportFile(CampagnProfileExportRequest request,  string userName);
        Task<bool> HandleExportFile2(CampagnProfileExportRequest request, string userName);
        Task<string> HandleExportFile3(CampagnProfileExportRequest request, string userName);
    }
}
