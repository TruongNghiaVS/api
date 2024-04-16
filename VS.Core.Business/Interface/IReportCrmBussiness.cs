using VS.core.Request;


namespace VS.Core.Business.Interface
{
    public interface IReportCrmBussiness
    {
      
        Task<string> ExportFileTaltime(
           CrmReportRequest request
         );

        Task<string> ExportStatusOverview(
          CrmReportRequest request
        ); 
        
        Task<string> ExportImpactStatusDetail(
          CrmReportRequest request
        );
        

        Task<string> GetsumoffTalktime(
         CrmReportRequest request
       );
        




    }
}
