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
        protected int offset;


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

        public async Task<ImpactDashboardOverviewReponse> GetALlOverView(ReportImpactRequest request)
        {
            try
            {
                using (var con = GetConnection())
                {
                    //if(request.To.HasValue)
                    //{
                    //    request.To = request.To.Value.
                    //}    
                    var result = await con.QueryAsync<ImpactDashboardOverviewReponseItem>(SqlContraint.GetVariable().ReportImpactOverview_GetAll, new
                    {
                        request.From,
                        request.To,
                        request.UserId,
                        request.StatusSearch,

                        request.LineCode,
                        request.VendorId
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
                    var result = await con.QueryAsync<ReportImpactItem>(
                    SqlContraint.GetVariable().ReportImpact_GetAll,
                   new
                   {
                       request.UserId,
                       request.LineCode,
                       request.VendorId,

                       request.StatusSearch,
                       request.From,
                       request.To
                   },
                    commandType: CommandType.StoredProcedure
                   );

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


        public async Task<ReportImpactReponse> ExportImpactData(ReportImpactRequest request)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ReportImpactItemEx>(
                    SqlContraint.GetVariable().ExportImpactData_GetAll,
                   new
                   {
                       request.UserId,
                       request.LineCode,
                       request.StatusSearch,
                       request.VendorId,
                       request.From,
                       request.To
                   },
                   commandType: CommandType.StoredProcedure
                   );

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

        protected void ProcessInputPaging(ref int page, ref int limit, out int offset)
        {
            page = page <= 0 ? 1 : page;
            if (limit <= 0)
                limit = 20;
            if (limit > 1000)
                limit = 100;
            offset = (page - 1) * limit;
        }
        public async Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var sqlName = SqlContraint.GetVariable().ReportTalkTime_getAll;
                    request.OrderBy = " d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItem>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            request.Token,
                            request.From,
                            request.VendorId,
                            request.To,
                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    if (dataFirst != null)
                    {
                        total = dataFirst.TotalRecord;
                    }
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = total
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        //GetAllRecordingFile


        public async Task<ReportCDRReponse> getAllCall(ReportCallRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var sqlName = SqlContraint.GetVariable().ReportgetAllCall;
                    request.OrderBy = " d.calldate desc ";
                    var result = await con.QueryAsync<ReportCallItem>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            request.Token,
                            request.From,
                            request.VendorId,
                            request.To,
                            request.NoAgree,
                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    if (dataFirst != null)
                    {
                        total = dataFirst.TotalRecord;
                    }
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = total
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        //GetAllRecordingFile



        public async Task<ReportCDRReponse> ExportRecordingFile(ReportCDRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {


                using (var con = GetConnection())
                {
                    var from = request.From;
                    if (request.TimeFrom1 > -1)
                    {
                        from = from.Value.AddSeconds(request.TimeFrom1.Value);
                    }

                    var to = request.To;
                    if (request.TimeFrom2 > -1)
                    {
                        to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 0, 0, 0).AddSeconds(request.TimeFrom2.Value);
                    }
                    else
                    {

                        to = request.To;
                    }

                    var TimeTalkBegin = 0;
                    if (request.TimeTalkBegin > -1)
                    {
                        TimeTalkBegin = request.TimeTalkBegin.Value;
                    }

                    var TimeTalkEnd = 600;
                    if (request.TimeTalkEnd > -1)
                    {
                        TimeTalkEnd = request.TimeTalkEnd.Value;
                    }

                    var sqlName = SqlContraint.GetVariable().RecordingFileExport_getAll;
                    request.OrderBy = " d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItemExport>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            request.Token,
                            request.VendorId,
                            request.NoAgree,
                            from,
                            to,
                            TimeTalkBegin,
                            TimeTalkEnd,
                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    //if (dataFirst != null)
                    //{
                    //    total = dataFirst.TotalRecord;
                    //}
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = 0
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<ReportCDRReponse> ExportRecordingFileNo(ReportNoCDRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {


                using (var con = GetConnection())
                {
                    var from = request.From;
                    if (request.TimeFrom1 > -1)
                    {
                        from = from.Value.AddSeconds(request.TimeFrom1.Value);
                    }

                    var to = request.To;
                    if (request.TimeFrom2 > -1)
                    {
                        to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 0, 0, 0).AddSeconds(request.TimeFrom2.Value);
                    }
                    else
                    {

                        to = request.To;
                    }

                    var TimeTalkBegin = 0;
                    if (request.TimeTalkBegin > -1)
                    {
                        TimeTalkBegin = request.TimeTalkBegin.Value;
                    }

                    var TimeTalkEnd = 600;
                    if (request.TimeTalkEnd > -1)
                    {
                        TimeTalkEnd = request.TimeTalkEnd.Value;
                    }

                    var sqlName = SqlContraint.GetVariable().RecordingFileExportNo_getAll;
                    request.OrderBy = " d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItemExport>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            request.Token,
                            request.NoAgree,
                            request.VendorId,
                            from,
                            to,
                            TimeTalkBegin,
                            TimeTalkEnd,
                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    //if (dataFirst != null)
                    //{
                    //    total = dataFirst.TotalRecord;
                    //}
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = 0
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }




        public async Task<ReportCDRReponse> getAllRecordingFileWithNo(ReportNoCDRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var from = request.From;
                    if (request.TimeFrom1 > -1)
                    {
                        from = from.Value.AddSeconds(request.TimeFrom1.Value);
                    }

                    var to = request.To;
                    if (request.TimeFrom2 > -1)
                    {
                        to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 0, 0, 0).AddSeconds(request.TimeFrom2.Value);
                    }
                    else
                    {

                        to = request.To;
                    }

                    var TimeTalkBegin = 0;
                    if (request.TimeTalkBegin > -1)
                    {
                        TimeTalkBegin = request.TimeTalkBegin.Value;
                    }

                    var TimeTalkEnd = 600;
                    if (request.TimeTalkEnd > -1)
                    {
                        TimeTalkEnd = request.TimeTalkEnd.Value;
                    }


                    var sqlName = SqlContraint.GetVariable().RecordingFile_getAllWithNo;
                    request.OrderBy = " d.calldate desc ";
                    var result = await con.QueryAsync<ReportCDRItem>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            request.Token,
                            request.VendorId,
                            TimeTalkEnd,
                            request.NoAgree,

                            TimeTalkBegin,
                            from,
                            to,

                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    if (dataFirst != null)
                    {
                        total = dataFirst.TotalRecord;
                    }
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = total
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<ReportCDRReponse> GetAllRecordingFile(ReportCDRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var sqlName = SqlContraint.GetVariable().RecordingFile_getAllv3;
                    request.OrderBy = " d.calldate desc ";

                    var from = request.From;
                    if (request.TimeFrom1 > -1)
                    {
                        from = from.Value.AddSeconds(request.TimeFrom1.Value);
                    }

                    var to = request.To;
                    if (request.TimeFrom2 > -1)
                    {
                        to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 0, 0, 0).AddSeconds(request.TimeFrom2.Value);
                    }
                    else
                    {

                        to = request.To;
                    }

                    var TimeTalkBegin = 0;
                    if (request.TimeTalkBegin > -1)
                    {
                        TimeTalkBegin = request.TimeTalkBegin.Value;
                    }

                    var TimeTalkEnd = 600;
                    if (request.TimeTalkEnd > -1)
                    {
                        TimeTalkEnd = request.TimeTalkEnd.Value;
                    }


                    var result = await con.QueryAsync<ReportCDRItem>(sqlName,
                        new
                        {
                            request.Disposition,
                            request.PhoneLog,
                            request.LineCode,
                            TimeTalkEnd,

                            TimeTalkBegin,
                            from,
                            to,

                            request.Token,
                            request.VendorId,
                            request.NoAgree,

                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    if (dataFirst != null)
                    {
                        total = dataFirst.TotalRecord;
                    }
                    var reponse = new ReportCDRReponse()
                    {
                        Data = result?.ToList(),
                        Total = total
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<FirstCallLastCallReponse> getAllFirstLastCall(FirstCallLastCallRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var sqlName = SqlContraint.GetVariable().Call_FirstLast;
                    request.OrderBy = " d.calldate desc ";



                    var result = await con.QueryAsync<FirstCallLastCallReponse>(sqlName,
                        new
                        {
                            request.From,
                            request.To,
                            request.Token,
                            request.VendorId,
                            request.Limit,
                            request.Page,
                            request.OrderBy,
                            request.UserId
                        }, commandType: CommandType.StoredProcedure);

                    var dataFirst = result.FirstOrDefault();
                    var total = 0;
                    if (dataFirst != null)
                    {
                        total = dataFirst.TotalRecord;
                    }
                    var reponse = new FirstCallLastCallReponse()
                    {
                        Data = result?.ToList(),
                        Total = total
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
                using (var con = GetConnection())
                {
                    var sql = " select count(d.calldate) as 'Total',d.disposition as type  from ReportTalkTime d where d.linecode = @LineCode group by d.disposition ";
                    var result = await con.QueryAsync<GetOverViewInfoReponseItem>(sql, new { Code = "", LineCode = request.LineCode });
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
                using (var con = GetConnection())
                {
                    var sql = " SELECT sum(cast(d.DurationReal as decimal)) as 'Duration',sum(cast(d.DurationBill as decimal)) as 'billsec', d.disposition from ReportTalkTime d where d.lineCode = @linecode  group by d.disposition";
                    var result = await con.QueryAsync<GetOverViewTalkingItem>(sql, new { Code = "", linecode = request.LineCode });
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
                        " WHERE d.src = 9000  And d.lastapp ='Dial' and d.disposition = 'ANSWERED' order by d.calldate desc ";
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

        public async Task<int> GetReportOverviewAgrree(GetReportOverviewAgrreeRequest request)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ReportNoAgreeRequest>(SqlContraint.GetVariable().ReportCampaignProfile, new
                    {
                        request.LineCode,
                        request.UserId
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
