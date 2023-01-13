using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

using VS.core.Request;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IConfiguration _configuration;



        public ReportRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection(_configuration.GetConnectionString("stringConnect"));
            con.Open();
            return con;
        }


        protected IDbConnection GetMysqlConnection()
        {
            var con = new MySqlConnection(_configuration.GetConnectionString("mysqlStringConnect"));
            con.Open();
            return con;
        }

        public async Task<ImpactDashboardOverviewReponse> GetALlOverView(int user_id = 0)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ImpactDashboardOverviewReponseItem>(SqlContraint.GetVariable().ReportImpactOverview_GetAll, new
                    {
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();

                    var reponse = new ImpactDashboardOverviewReponse()
                    {
                        Data = result?.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ReportImpactItem>(SqlContraint.GetVariable().ReportImpact_GetAll, new
                    {
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();

                    var reponse = new ReportImpactReponse()
                    {
                        Data = result?.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request)
        {
            try
            {
                using (var con = GetMysqlConnection())
                {
                    var sql = "SELECT d.src, d.calldate, d.dst, d.disposition, d.lastapp, d.billsec, d.duration, d.recordingfile FROM `cdr` d WHERE d.src = 9005  And d.lastapp ='Dial' order by d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItem>(sql, new { Code = "" });
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request)
        {
            try
            {
                using (var con = GetMysqlConnection())
                {
                    var sql = " select COUNT(d.calldate) as Total, d.disposition as Type from cdr d where d.src = '9005' and d.lastapp = 'Dial' group by d.disposition ";
                    var result = await con.QueryAsync<GetOverViewInfoReponseItem>(sql, new { Code = "" });
                    var reponse = new GetOverViewInfoReponse()
                    {
                        Data = result.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request)
        {
            try
            {
                using (var con = GetMysqlConnection())
                {
                    var sql = " SELECT sum(d.duration) as 'Duration',  sum(d.billsec) as 'billsec', d.disposition from cdr d where d.src = 9005 and d.lastapp =\"Dial\" group by d.disposition";
                    var result = await con.QueryAsync<GetOverViewTalkingItem>(sql, new { Code = "" });
                    var reponse = new GetOverViewTalkingItemReponse()
                    {
                        Data = result.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request)
        {
            try
            {
                using (var con = GetMysqlConnection())
                {
                    var sql = " SELECT d.src, d.calldate, d.dst, d.disposition, d.lastapp, d.billsec, d.duration, d.recordingfile FROM `cdr` d" +
                        " WHERE d.src = 9005  And d.lastapp ='Dial' and d.disposition = 'ANSWERED' order by d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItem>(sql, new { Code = "" });
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList()
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<int> GetReportOverviewAgrree()
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ReportNoAgreeRequest>(SqlContraint.GetVariable().ReportCampaignProfile, new
                    {
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();

                    int totalCount = 0;
                    try
                    {
                        totalCount = fistElement.Total;
                    }
                    catch (Exception e)
                    {

                    }
                    return totalCount;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
