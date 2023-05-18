using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ReportTalkTimeGroupByDayRepository : RepositoryBase<ReportTalkTimeGroupByDay>, IReportTalkTimeGroupByDay
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "ReportTalkTimeGroupByDay";
        public ReportTalkTimeGroupByDayRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(ReportTalkTimeGroupByDay entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            if (entity.PerPercent > 0)
            {
                entity.PerPercent = Math.Round(entity.PerPercent.Value, 2);
            }
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
                    var result = await _con.ExecuteAsync(_Sql.ReportTalkTimeGroupByDay_insert, par, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public async Task<ReportTalkTimeGroupByDay> GetLineGroupBydate(string lineCode,
            int day, int month, int year)
        {
            using (var con = GetConnection())
            {
                var sql = " SELECT * FROM  ReportTalkTimeGroupByDay WHERE dayR = @dayR  and monthR = @monthR and yearR = @yearR and lineCode = @lineCode ";
                var result = await con.QuerySingleOrDefaultAsync<ReportTalkTimeGroupByDay>(sql, new
                {
                    dayR = day,
                    monthR = month,
                    yearR = year,
                    lineCode = @lineCode
                });
                return result;
            }
        }

        public async Task<Account> GetByLineCode(string lineCode)
        {
            using (var con = GetConnection())
            {
                var sql = "select  top 1 * from Employees d where d.LineCode = @LineCode";
                var result = await con.QuerySingleOrDefaultAsync<Account>(sql, new { LineCode = lineCode });
                return result;
            }
        }
        public async Task<GetAllRecordGroupByLineCodeReponse> ProcessingReportGroupByDay(GetAllRecordGroupByLineCodeRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var timeSelect = request.TimeSelect;
                    var result = await con.QueryAsync<ProcessingCalTimeIndexModel>(_Sql.Sp_ProcessingCalTimeIndexModel_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,

                        request.TimeSelect,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    var query = result.GroupBy(x => new { x.LineCode });

                    var value = query.ToList();

                    foreach (var s in value)
                    {
                        var values = s.ToList();
                        var sumCall = 0;
                        var lineCode = s.Key.LineCode;
                        var sumNoAgree = 0;
                        double perPercent = 0.0;
                        var sumAn = 0;
                        var sumNoBussy = 0;
                        var sumNOCancel = 0;
                        var sumNOAswer = 0;
                        var sumNoFail = 0;
                        var sumNOChanel = 0;
                        var sumNOServe = 0;
                        var sumNoBusy = 0;
                        double timeWaiting = 0.0;
                        decimal? timcall = 0;
                        decimal? DurationBill = 0;
                        decimal? Duration = 0;
                        decimal? Billsec = 0;
                        int yearR = timeSelect.Value.Year;
                        int monthR = timeSelect.Value.Month;
                        int dayR = timeSelect.Value.Day;
                        foreach (var itemchild in values)
                        {
                            if (itemchild.Disposition == "ANSWERED")
                            {
                                Duration = itemchild.Duration;
                                sumAn = itemchild.Total;
                            }
                            if (itemchild.Disposition == "NO ANSWER")
                            {
                                sumNOAswer = itemchild.Total;
                            }
                            if (itemchild.Disposition == "FAILED")
                            {
                                sumNoFail = itemchild.Total;
                            }
                            if (itemchild.Disposition == "BUSY")
                            {
                                sumNoBusy = itemchild.Total;
                            }
                            Billsec += itemchild.Billsec;
                            sumCall += itemchild.Total;
                        }
                        if (sumCall > 0)
                        {
                            perPercent = (double)sumAn / (double)sumCall * 100;
                        }
                        else
                            perPercent = 0;
                        perPercent = Math.Round(perPercent, 1);
                        var groupItem = new ReportTalkTimeGroupByDay()
                        {
                            LineCode = lineCode,
                            BusinessTime = new DateTime(yearR, monthR, dayR),
                            CreateAt = DateTime.Now,
                            DayR = dayR,
                            MonthR = monthR,
                            YearR = yearR,
                            SumAn = sumAn,
                            SumCall = sumCall,
                            SumNOAswer = sumNOAswer,
                            SumNoFail = sumNoFail,
                            PerPercent = perPercent,
                            SumNoBussy = sumNoBusy,
                            SumNOChanel = sumNOChanel,
                            SumNOCancel = sumNOCancel,
                            SumNOServe = sumNOServe,
                            TimeTalking = Duration,
                            Timcall = Billsec,
                            TimeWaiting = Billsec - Duration,
                            CreatedBy = "1",
                            Deleted = false,
                            UpdateAt = DateTime.Now

                        };
                        var groupItemgroupByLineCode = await GetLineGroupBydate(lineCode, dayR, monthR, yearR);
                        var usergetByLinecode = await this.GetByLineCode(lineCode);
                        if (groupItemgroupByLineCode == null)
                        {
                            if (usergetByLinecode != null)
                            {
                                groupItem.VendorId = usergetByLinecode.VendorId;
                            }
                            await Add(groupItem);
                        }
                        else
                        {
                            groupItemgroupByLineCode.BusinessTime = new DateTime(yearR, monthR, dayR);
                            groupItemgroupByLineCode.SumAn = sumAn;
                            groupItemgroupByLineCode.SumCall = sumCall;
                            groupItemgroupByLineCode.SumNOAswer = sumNOAswer;
                            groupItemgroupByLineCode.SumNoFail = sumNoFail;
                            groupItemgroupByLineCode.PerPercent = perPercent;
                            groupItemgroupByLineCode.SumNoBussy = sumNoBusy;
                            groupItemgroupByLineCode.SumNOChanel = sumNOChanel;
                            groupItemgroupByLineCode.SumNOCancel = sumNOCancel;
                            groupItemgroupByLineCode.SumNOServe = sumNOServe;
                            groupItemgroupByLineCode.Timcall = Billsec;
                            groupItemgroupByLineCode.TimeTalking = Duration;
                            groupItemgroupByLineCode.TimeWaiting = Billsec - Duration;
                            if (usergetByLinecode != null)
                            {
                                groupItemgroupByLineCode.VendorId = usergetByLinecode.VendorId;
                            }
                            else
                            {

                            }
                            await Update(groupItemgroupByLineCode);
                        }
                    }
                    return new GetAllRecordGroupByLineCodeReponse();


                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<GetAllRecordGroupByLineCodeExportReponse> Export(GetAllRecordGroupByLineCodeExportRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GetAllRecordGroupByLineCodeExportIndexModel>(_Sql.GetAllRecordGroupByLineCodeExport_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.UserId,
                        request.VendorId,
                        request.LineCode,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    //var fistElement = result.FirstOrDefault();
                    //var totalRecord = 0;
                    //if (fistElement != null)
                    //{
                    //    totalRecord = fistElement.TotalRecord;
                    //}
                    var reponse = new GetAllRecordGroupByLineCodeExportReponse()
                    {
                        Total = 0,

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
        public async Task<GetAllRecordGroupByLineCodeReponse> GetAll(GetAllRecordGroupByLineCodeRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GetAllRecordGroupByLineCodeIndexModel>(_Sql.GetAllRecordGroupByLineCode_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.VendorId,
                        request.LineCode,
                        request.Limit,
                        request.UserId,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new GetAllRecordGroupByLineCodeReponse()
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


        public async Task<GetOverViewDashboardReponse> GetOverViewDashBoard(GetOverViewDashboard request)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GetOverViewDashBoardIndexModel>(_Sql.GetGetOverViewDashBoard_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.LineCode,
                        request.VendorId,
                        request.UserId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();

                    var reponse = new GetOverViewDashboardReponse()
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

        public async Task<int> Update(ReportTalkTimeGroupByDay entity)
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
                    var result = await _con.ExecuteAsync(_Sql.ReportTalkTimeGroupByDay_update, par, commandType: CommandType.StoredProcedure);

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
