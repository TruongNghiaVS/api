using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ExportFileRepository : RepositoryBase<CampagnProfile>, IExportFileRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Employees";
        public ExportFileRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CampagnProfileExportReponse> GetAllCampagnProfile(CampagnProfileExportRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnProileExportIndexModel>(_Sql.CampagnProfileGetAllExport, new
                    {
                        request.From,
                        request.To,
                        request.VendorId,
                        request.UserId,
                        request.CampaignId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = result.Count();
                   
                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

      
    }
}
