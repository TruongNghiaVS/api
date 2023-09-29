using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class WorkplaceNotedRepository : RepositoryBase<WorkplaceNoted>, IWorkplaceNotedRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "WorkplaceNoted";
        public WorkplaceNotedRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(WorkplaceNoted entity)
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
                    var result = await _con.ExecuteAsync(_Sql.WorkplaceNoted_Insert, new
                    {


                    }, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public async Task<WorkplaceNotedReponse> GetALl(WorkplaceNotedRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<DataQCViewModel>(_Sql.WorkplaceNoted_Insert, new
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
                    var reponse = new WorkplaceNotedReponse()
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


        public Task<int> Update(WorkplaceNoted entity)
        {
            throw new NotImplementedException();
        }
    }
}
