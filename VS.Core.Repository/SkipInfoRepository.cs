using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Repository
{
    public class SkipInfoRepository : RepositoryBase<SkipInfo>, ISkipInfoRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "SkipInfo";
        public SkipInfoRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> AddSkip(SkipInfo entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                nameof(entity.UpdatedBy),
                nameof(entity.UpdateAt),
                nameof(entity.Cmnd),
                nameof(entity.Id),
                nameof(entity.CreateAt),
                nameof(entity.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.SkipInfo_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public async Task<List<SkipInfo>> GetALl(SkipInfoSerarchRequest request)
        {

            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<SkipInfo>(_Sql.SkipInfo_getAll, new
                    {

                        request.NoAgreement,

                    }, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        return result.ToList();
                    }
                    return new List<SkipInfo>();
                }
            }
            catch (Exception e)
            {
                return new List<SkipInfo>();
            }
        }

    }
}
