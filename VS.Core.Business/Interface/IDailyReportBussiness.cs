using VS.core.Request;

namespace VS.Core.Business.Interface
{
    public interface IDailyReportBussiness
    {
        Task<bool> HandleExportFile(CampagnProfileExportRequest request,  string userName);
        //Task<bool> HandleExportFile2(CampagnProfileExportRequest request, string userName);
        Task<string> ExportFileTotal(CampagnProfileExportRequest request, string userName);

        Task<string> ExportFileExcel(CampagnProfileExportRequest request, string userName);


    }
}
