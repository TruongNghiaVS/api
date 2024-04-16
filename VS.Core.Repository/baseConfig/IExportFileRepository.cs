using VS.core.Request;
using VS.Core.dataEntry.User;


namespace VS.Core.Repository.baseConfig
{
    public interface IExportFileRepository : IGenericRepository<CampagnProfile>
    {
        Task<CampagnProfileExportReponse> GetAllCase(CampagnProfileExportRequest request);
        Task<CampagnProfileExportReponse> GetAllCasev2(CampagnProfileExportRequest request);

        Task<CampagnProfileExportReponse> ExprotCrmTalkTime(
                   CrmReportRequest request


            );
        Task<CampagnProfileExportReponse> ExprotCrmByStatus(
               CrmReportRequest request


        );

        Task<CampagnProfileExportReponse> ExprotCrmByStatusDetail(

              CrmReportRequest request


       );

        Task<CampagnProfileExportReponse> GetSumupTalktime(

            CrmReportRequest request


     );
    }




}
