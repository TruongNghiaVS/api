using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.Report
{
    public class LoginReportReposiotry : RepositoryBase<LoginReport>, IloginReportRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string tableName = "loginReport";
        public LoginReportReposiotry(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(LoginReport entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {

                nameof(entity.Id),
                nameof(entity.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.LoginReport_insert, par, commandType: CommandType.StoredProcedure);

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
                var result = await con.QuerySingleOrDefaultAsync<LoginReportIndexModel>(sql, new { Code = code });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<LoginReportIndexModel>(_Sql.LoginReport_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.UserName,
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
                    var reponse = new LoginReportReponse()
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
        public async Task<List<LoginReportMiraeIndexModel>> ExportLogin(LoginReportSerarchRequest loginRequest)
        {
            var dataReponse = new List<LoginReportMiraeIndexModel>();
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.QueryAsync<LoginReportMiraeIndexModel>("sp_getTimeReportLogin", new
                    {
                        timeHandle = loginRequest.From
                    }, commandType: CommandType.StoredProcedure);

                    var listData = new List<LoginReportMiraeIndexModel>();

                    var listTotal = result.ToList();
                    foreach (var item in listTotal)
                    {
                        if (item.Typedata == 1)
                        {
                            item.CheckIn = item.TimeBusiness;
                            listData.Add(item);
                        }
                    }
                    foreach (var item in listData)
                    {
                        var itemLougout = listTotal
                            .Where(x => x.Typedata == 0 && x.UserName == item.UserName)
                            .FirstOrDefault();
                        if (itemLougout != null)
                        {
                            item.Checkout = itemLougout.TimeBusiness;
                        }
                    }
                    dataReponse = listData;
                }
            }
            catch (Exception e)
            {
                dataReponse = new List<LoginReportMiraeIndexModel>();

            }
            return dataReponse;


        }
        public async Task<int> Update(LoginReport entity)
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
