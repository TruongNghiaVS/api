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
    public class ProfileCampagnRepository : RepositoryBase<CampagnProfile>, IProfileCampagnRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string tableName = "CampaignProfile";
        public ProfileCampagnRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(CampagnProfile model)
        {
            model.CreateAt = DateTime.Now;
            model.UpdateAt = DateTime.Now;
            var par = GetParams(model, new string[] {
                nameof(model.UpdatedBy),
                nameof(model.UpdateAt),
                nameof(model.Id),

                nameof(model.CreateAt),
                nameof(model.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.CampaignProfile_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }
        public async Task<int> UpdateSkipData(CampagnProfile entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Campaign_update_caseSkip, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }


        public async Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try

            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<ProileIndexModel>(_Sql.CampaignProfile_getAll, new
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
                        request.UserId
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

        public async Task<GetAllProfileByCampangReponse> ExportDataByCampaign(GetAllProfileByCampang request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try

            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnProileExportIndexModel>(_Sql.CampaignProfile_exportData, new
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
                        request.UserId
                    }, commandType: CommandType.StoredProcedure);

                    var reponse = new GetAllProfileByCampangReponse()
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


        public async Task<int> ResetCase(CampagnProfile entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Sp_CampaignProfile_resetCase, par, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }
        public async Task<int> Update(CampagnProfile entity)
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
                    var result = await _con.ExecuteAsync(_Sql.CampaignProfile_update, par, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }



        public async Task<int> UpdateSkip(CampagnProfile entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                nameof(entity.UpdateAt),
                nameof(entity.CreateAt),
                nameof(entity.ColorCode),
                nameof(entity.Deleted),
                nameof(entity.CreatedBy)
            });
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.CampaignProfile_update_skip, par, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }

        public async Task<int> ImportUpdate(CampagnProfile entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Sp_CampaignProfile_import_update, par, commandType: CommandType.StoredProcedure);
                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }
        }

        public async Task<bool> AssignedTask(string profileId, string userId)
        {
            using (var con = GetConnection())
            {
                var sql = "update CampaignProfile set assignee =@assignee  where Id =@id";
                var result = await con.ExecuteAsync(sql, new { assignee = userId, id = profileId });

                if (result > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public async Task<bool> DeleteMutipleCam(List<string> dataDelete, string requestId)
        {
            if (dataDelete.Count < 1)
                return true;
            using (var con = GetConnection())
            {
                string listOfIdsJoined = "(" + String.Join(",", dataDelete.ToArray()) + ")";
                var sql = "update CampaignProfile set Deleted = 1  where CampaignId = @CampaignId and  Id in @dataDelete";
                var result = await con.ExecuteAsync(sql, new
                {
                    dataDelete = listOfIdsJoined,
                    CampaignId = requestId
                });

                if (result > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public async Task<List<CampagnProfile>> GetALLAsiggnee(GetAllProfileByCampang request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<CampagnProfile>(_Sql.CampaignProfile_getAll, new
                    {
                        request.Id,
                        request.Token,
                        request.From,
                        request.To,
                        request.Limit,
                        request.Page,
                        request.TypegetData,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                return new List<CampagnProfile>();
            }
        }

        public async Task<bool> HanldleCase(int? id, bool? resetCase, bool? skipp)
        {
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.CampaignProfile_handleCase, new
                    {
                        id,
                        resetCase,
                        skipp
                    }, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception e)
            {
                return false;

            }

        }
        public async Task<CampagnProfile> GetBYNoAreeMentLasted(string profileId)
        {
            using (var con = GetConnection())
            {

                var sql = "select top 1 * from CampaignProfile e " + " WHERE NoAgreement = @profileId order by e.UpdateAt desc ";


                var result = await con.QuerySingleOrDefaultAsync<CampagnProfile>(sql, new { profileId = profileId });
                if (result == null)
                {
                    return null;
                }
                return result;
            }
        }
        public async Task<CampagnProfile> GetByNoAgreement(string profileId, string campanId = null)

        {
            using (var con = GetConnection())
            {

                var sql = "SELECT * FROM CampaignProfile " + " WHERE NoAgreement = @profileId ";

                if (!string.IsNullOrEmpty(campanId))
                {
                    sql = "SELECT * FROM CampaignProfile " + " WHERE NoAgreement = @profileId and campaignId = @campanId";
                }
               ;

                try
                {
                    var result = await con.QuerySingleOrDefaultAsync<CampagnProfile>(sql, new { profileId = profileId, campanId = campanId });
                    if (result == null)
                    {
                        return null;
                    }
                    return result;
                }
                catch (Exception e)
                {

                    return null;
                }

            }
        }





        public async Task<CampagnProfile> GetProfileByNoCMND(string noNational)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT top 1 * FROM CampaignProfile " + " WHERE (isnull(Deleted,0) =0) and  (NoAgreement = @profileId or NationalId  = @profileId  or  MobilePhone = @profileId) order by id desc";
                var result = await con.QuerySingleOrDefaultAsync<CampagnProfile>(sql, new { profileId = noNational });

                if (result == null)
                {
                    return null;
                }
                return result;
            }
        }


        public async Task<List<CampagnProfile>> GetAllInfoSkipp(string noNational)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM CampaignProfile " + " WHERE (isnull(Deleted,0) =0) and  (NoAgreement = @profileId or NationalId  = @profileId  or  MobilePhone = @profileId) order by id desc";
                var result = await con.QueryAsync<CampagnProfile>(sql, new { profileId = noNational });

                if (result == null)
                {
                    return new List<CampagnProfile>();
                }
                return result.ToList();
            }
        }


    }
}
