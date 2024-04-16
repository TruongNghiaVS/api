using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ExportFileRepository : RepositoryBase<CampagnProfile>, IExportFileRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Employees";
        public ExportFileRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<CampagnProfileExportReponse> GetAllCase(CampagnProfileExportRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnProileExportIndexModel>(_Sql.CampagnProfileGetAllExport, new
                    {
                        request.From,
                        request.To,
                        request.VendorId,
                        request.UserId,
                        request.CampaignId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;

                    if(fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CampagnProileExportIndexModel> ();
                    } 
                        
                   
                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<CampagnProfileExportReponse> GetAllCasev2(CampagnProfileExportRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnProileExportIndexModel>(
                        _Sql.CampagnProfileGetAllExportv2, new
                    {
                        request.From,
                        request.To,
                        request.VendorId,
                        request.UserId,
                        request.TypegetData,
                        request.CampaignId,
                        request.Limit,
                        request.Page,
                        request.GroupId,
                        request.MemberId,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);
                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;
                    if (fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CampagnProileExportIndexModel>();
                    }
                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<CampagnProfileExportReponse> ExprotCrmTalkTime(
                    CrmReportRequest request


             )
        {
           

            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CrmReprotIndexModel>(_Sql.CrmReport_Talktime, new
                    {
                        request.From,
                        request.UserId,
                        request.To,
                      
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;

                    if (fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CrmReprotIndexModel>();
                    }


                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<CampagnProfileExportReponse> ExprotCrmByStatus(
                  CrmReportRequest request


           )
        {


            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CrmReprotStatusIndexModel>(_Sql.CrmReport_BystatusFolow, new
                    {
                        request.From,
                        request.UserId,
                        request.To,

                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;

                    if (fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CrmReprotStatusIndexModel>();
                    }


                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<CampagnProfileExportReponse> ExprotCrmByStatusDetail(
             CrmReportRequest request


      )
        {


            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CrmReprotStatusIndexModel>(_Sql.CrmReport_ByDetailGroupStatus, new
                    {
                        request.From,
                        request.UserId,
                        request.To
                    

                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;

                    if (fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CrmReprotStatusIndexModel>();
                    }


                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<CampagnProfileExportReponse> GetSumupTalktime(
             CrmReportRequest request


      )
        {


            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CrmReportTalktimeIndexModel>(_Sql.CrmReport_CrmReport_SumupTalktime, new
                    {
                        request.From,
                        request.UserId,
                        request.To,

                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var numberRecord = 0;

                    if (fistElement != null)
                    {
                        numberRecord = result.Count();
                    }
                    else
                    {
                        result = new List<CrmReportTalktimeIndexModel>();
                    }


                    var reponse = new CampagnProfileExportReponse()
                    {
                        Data = result,
                        NumberRecord = numberRecord
                    };
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
