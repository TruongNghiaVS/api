using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class ProfileCampagnRepository : RepositoryBase<Profile>, IProfileCampagnRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string tableName = "CampaignProfile";
        public ProfileCampagnRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> AddAsync(Profile model)
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
                        request.Limit,
                        request.Page,
                        request.TypegetData,
                        request.OrderBy,
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
        public async Task<int> UpdateAsyn(Profile entity)
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

        public async Task<List<Profile>> GetALLAsiggnee(GetAllProfileByCampang request)
        {
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<Profile>(_Sql.CampaignProfile_getAll, new
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
                return new List<Profile>();
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


        public async Task<Profile> GetByNoAgreement(string profileId)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM CampaignProfile " + " WHERE NoAgreement = @profileId";
                var result = await con.QuerySingleOrDefaultAsync<Profile>(sql, new { profileId = profileId });

                if (result == null)
                {
                    return null;
                }
                return result;
            }
        }

    }
}
