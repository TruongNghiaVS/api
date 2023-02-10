using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ReportTalkTimeRepository : RepositoryBase<ReportTalkTime>, IReportTalkTimeRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "ReportTalkTime";
        public ReportTalkTimeRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> AddAsync(ReportTalkTime entity)
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
                    var result = await _con.ExecuteAsync(_Sql.ReportTalkTime_insert, par, commandType: CommandType.StoredProcedure);

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

        public async Task<int> DeleteAll()
        {
            using (var con = GetConnection())
            {
                var sql = "delete from  " + _baseTable + " WHERE 1 =1";
                var result = await con.QuerySingleOrDefaultAsync(sql);

                return 1;
            }
        }

        public async Task<ReportTalkTimeReponse> GetALl(ReportTalkTimeRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CallLogIndexModel>(_Sql.ReportTalkTime_getAll, new
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
                    var reponse = new ReportTalkTimeReponse()
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
        public async Task<int> UpdateAsyn(ReportTalkTime entity)
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
                    var result = await _con.ExecuteAsync(_Sql.ReportTalkTime_update, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }

        public async Task<IEnumerable<ReportQuerryTaltimeIndex>> HandlelFileRecording(HandlelFileRecordingRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetMysqlConnection2())
                {
                    var sqlQuerry = "SELECT d.src AS 'LineCode', d.dst AS  'PhoneLog', d.linkedid AS 'Linkedid', d.calldate,  d.disposition, d.billsec AS 'DurationBill', d.duration AS 'Duration', d.recordingfile AS 'FileRecording'  FROM cdr d WHERE  d.lastapp = 'Dial'";
                    var result = await con.QueryAsync<ReportQuerryTaltimeIndex>(sqlQuerry, new
                    {
                        request.Token,
                        request.From,
                        request.To,

                        request.Limit,
                        request.Page,
                        request.OrderBy
                    });
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ReportQuerryRecordingFileIndex> GetInfomationRecording(string likiedId)
        {

            try
            {
                using (var con = GetMysqlConnection3())
                {
                    var sqlQuerry = "SELECT * FROM recording_files d WHERE d.`file` LIKE '%" + likiedId + "%'";
                    var result = await con.QueryAsync<ReportQuerryRecordingFileIndex>(sqlQuerry, new
                    {

                    });

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    return fistElement;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        protected IDbConnection GetMysqlConnection2()
        {
            var con = new MySqlConnection(_configuration.GetConnectionString("mysqlStringConnect"));
            con.Open();
            return con;
        }

        protected IDbConnection GetMysqlConnection3()
        {
            var con = new MySqlConnection(_configuration.GetConnectionString("mysqlStringConnect2"));
            con.Open();
            return con;
        }




    }
}
