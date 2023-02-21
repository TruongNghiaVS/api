using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;
using static Dapper.SqlMapper;

namespace VS.Core.Repository
{
    public class CampagnRepository : RepositoryBase<Campagn>, ICampagnRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Campaign";
        public CampagnRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }


        public async Task<List<Campagn>> GetALlCampang()
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + "";
                var result = await con.QueryAsync<Campagn>(sql);
                if (result != null)
                {
                    return result.ToList();
                }
                return new List<Campagn>();
            }
        }
        public async Task<int> AddAsync(Campagn entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Campaign_insert, par, commandType: CommandType.StoredProcedure);

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
                var result = await con.QuerySingleOrDefaultAsync<Campagn>(sql, new { Code = code });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }


        public async Task<bool> UpdateOverView(string campaignId)
        {
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.campaign_UpdateOverview, new
                    {
                        campaignId
                    }, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }

        }
        public async Task<OverViewCampangnReponse> GetAllOverViewRe()
        {

            try
            {
                using (var con = GetConnection())

                {
                    var sqlText = "select * from OverViewCampangn";
                    var result = await con.QueryAsync<CampangnOverViewReModel>(sqlText, new
                    {

                    }, commandType: CommandType.Text);

                    var fistElement = result.FirstOrDefault();


                    var reponse = new OverViewCampangnReponse()
                    {
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


        public async Task<CampagnRequestReponse> GetALl(CampagnRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnIndexModel>(_Sql.Campaign_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
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
                    var reponse = new CampagnRequestReponse()
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

        public async Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnAsiggeeByCampagnIdIndexModel>(_Sql.Campaign_GetAllAsiggeeByCampagnId, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.CampaignId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    //if (fistElement != null)
                    //{
                    //    totalRecord = fistElement.TotalRecord;
                    //}
                    var reponse = new CampagnAsiggeeByCampagnIdReponse()
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

        public async Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnIndexModel>(_Sql.Campaign_getAll,
                        new
                        {
                            request.Token,
                            request.From,
                            request.To,
                            request.Limit,
                            request.Page,
                            request.OrderBy
                        },
                    commandType: CommandType.StoredProcedure);
                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new CampagnRequestReponse()
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

        public async Task<int> UpdateAsyn(Campagn entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Campaign_update, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }

        public async Task<GetOverviewCampaignModelByIdReponse> GetOverviewCampagnById(string campaignId)
        {

            var reponse = new GetOverviewCampaignModelByIdReponse();

            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GetOverviewCampaignModelById>(_Sql.Campaign_getOverviewbyId,
                    new
                    {
                        campaignId
                    },
                    commandType: CommandType.StoredProcedure);
                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    reponse.model = fistElement;
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
