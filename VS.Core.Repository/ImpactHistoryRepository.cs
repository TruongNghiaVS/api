using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.Campagn;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ImpactHistoryRepository : RepositoryBase<ImpactHistory>, IImpactHistoryRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "ImpactHistory";
        public ImpactHistoryRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(ImpactHistory entity)
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
                    var result = await _con.ExecuteAsync(_Sql.CampaignImpact_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public async Task<int> AddHistoryImpact(ImpactHistory entity)
        {

            var par = GetParams(entity, new string[] {

                nameof(entity.Id),

                nameof(entity.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Sp_ImpactHistory_Insert2, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }



        public async Task<bool> CheckDuplicate(string code)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE code = @Code";
                var result = await con.QuerySingleOrDefaultAsync<ImpactHistoryIndexModel>(sql, new { Code = code });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<List<ImpactHistory>> GetAllHistory(ImpactHistorySerarchRequest request)
        {

            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ImpactHistory>(_Sql.CampaignImpact_getAllHistory, new
                    {

                        request.ProfileId,

                    }, commandType: CommandType.StoredProcedure);

                    if (result != null)
                    {
                        return result.ToList();
                    }
                    return new List<ImpactHistory>();
                }
            }
            catch (Exception e)
            {
                return new List<ImpactHistory>();
            }
        }


        public async Task<ImpactHistoryReponse> GetFinal(ImpactHistorySerarchRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ImpactHistoryv2IndexModel>(_Sql.ImpactHistoryFinal_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.VendorId,
                        request.ProfileId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new ImpactHistoryReponse()
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

        public async Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ImpactHistoryIndexModel>(_Sql.CampaignImpact_getAllv2, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.ProfileId,
                        request.NoAgreement,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new ImpactHistoryReponse()
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

        public async Task<int> Update(ImpactHistory entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                nameof(entity.UpdateAt),
                nameof(entity.CreateAt),
                nameof(entity.Deleted),
                nameof(entity.CreatedBy)
            });
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.CampaignImpact_update, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }


    }
}
