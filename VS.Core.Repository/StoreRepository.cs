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
    public class StoreRepository : RepositoryBase<CampagnProfile>, IStoreRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string tableName = "CampaignProfile";
        public StoreRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try

            {
                using (var con = StoreConnection())
                {
                    var result = await con.QueryAsync<StoreProfileIndexModel>(_Sql.CampaignProfile_getAll, new
                    {
                        request.Id,
                        request.Token,
                        request.From,
                        request.To,
                        request.DpdMax,
                        request.DpdMin,
                        request.LineCode,
                        request.Limit,
                        request.NoAgreement,
                        request.Page,
                        request.PhoneSerach,
                        request.TypegetData,
                        request.OrderBy,
                        request.VendorId,
                        request.ColorCode,
                        request.UserId,
                        request.Cmnd
                    }, commandType: CommandType.StoredProcedure);
                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new GetAllProfileByCampangReponse()
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

        public async Task<GetAllStoreSkipReponse> GetAllSkip(string noAgreement)
        {
            try

            {
                using (var con = StoreConnection())
                {
                    var result = await con.QueryAsync<StoreSkipInfoIndexModel>(_Sql.Store_GetAllSkip, 
                    new
                    {
                        noAgreement
                    }, commandType: CommandType.StoredProcedure);
                    var reponse = new GetAllStoreSkipReponse()
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

       

        public async Task<bool> AddOrUpdate(CampagnProfile item)
        {
            try
            {
                using (var con = StoreConnection())
                {
                    var result = await con.QueryFirstAsync<StoreProfile>(_Sql.Store_GetByNoAgree,
                    new
                    {
                        
                    }, commandType: CommandType.StoredProcedure);
                    if (result == null)
                    {
                        result = new StoreProfile()
                        {
                            Id = "-1"
                        };
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

      
        public async Task<bool> AddOrUpdateSkip(StoreSkipInfo item)
        {
            item.CreateAt = DateTime.Now;
            item.UpdateAt = DateTime.Now;
            var par = GetParams(item, new string[] {
                nameof(item.UpdatedBy),
                nameof(item.UpdateAt),
                nameof(item.Id),
                nameof(item.CreateAt),
                nameof(item.Deleted)
            }, "Id");

            try
            {
                using (var _con = StoreConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Store_SkipInsertOrUpdate, par, commandType: CommandType.StoredProcedure);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public async Task<StoreProfile> GetByNoAgree(string noAgree)
        {
            try

            {
                using (var con = StoreConnection())
                {
                    var result = await con.QueryFirstAsync<StoreProfile>(_Sql.Store_GetByNoAgree,
                    new
                    {
                        noAgree
                    }, commandType: CommandType.StoredProcedure);
                    if(result==null)
                    {
                        result = new StoreProfile()
                        {
                            Id = "-1"
                        };
                    }
                   
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
      
        public Task<int> Update(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
