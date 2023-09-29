using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ViewRecordingRepository : RepositoryBase<ViewRecording>, IViewRecordingRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "ViewRecording";
        public ViewRecordingRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(ViewRecording entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                nameof(entity.UpdatedBy),
                nameof(entity.UpdateAt),
                nameof(entity.Id),
                nameof(entity.CreateAt),
                nameof(entity.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.ViewRecording_insert, new
                    {
                        entity.ProfileId,
                        entity.UserId

                    }, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public async Task<GetDashboardQcReponse> GetDataQc(GetDashboardQcRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<DataQCViewModel>(_Sql.Data_Qc, new
                    {
                        request.From,
                        request.To,
                        request.LineCode
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new GetDashboardQcReponse()
                    {
                        Total = totalRecord,

                        Data = result
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Task<int> Update(ViewRecording entity)
        {
            throw new NotImplementedException();
        }
    }
}
