using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class GroupEmployRepository : RepositoryBase<GroupEmployee>, IGroupEmployRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "GroupEmp";
        public GroupEmployRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> AddAsync(GroupEmployee entity)
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
                    var result = await _con.ExecuteAsync(_Sql.GroupEmpl_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }


        public async Task<int> AddMemberGroup(GroupMember entity)
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
                    var result = await _con.ExecuteAsync(_Sql.sp_GroupMember_Insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }



        public async Task<bool> CheckDuplicate(string userName)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE code = @userName";
                var result = await con.QuerySingleOrDefaultAsync<GroupEmployee>(sql, new { UserName = userName });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<int> Deletemember(int id)
        {
            using (var con = GetConnection())
            {
                var sql = "delete FROM  groupmember  WHERE  memberid = @id";
                var result = await con.QuerySingleOrDefaultAsync<int>(sql, new { id = id });
                return result;
            }
        }



        public async Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request)
        {
            if (request == null)
            {
                request = new GroupEmployeeRequest();
            }
            int page = request.Page;
            int limit = request.Limit;
            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {

                    //displayName
                    var result = await con.QueryAsync<SelectIndexModel>(_Sql.GroupEmpl_getAllManager, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.VendorId,
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
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public async Task<EmployeeSearchReponse> getMemberByGroup(MemberGroupByIdRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GroupEmployeeViewIndexModel>(_Sql.sp_GetAllMeberByGroupId, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.GroupId,
                        request.VendorId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;

                    var reponse = new EmployeeSearchReponse()
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

        public async Task<GroupEmployeeHaveNotInGroupRequestReponse> GetAllMeberHaveNotGroup(GroupEmployeeHaveNotInGroupRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GroupEmployeeNotInGroupIndexModel>(_Sql.Sp_GetAllMeberHaveNotGroup, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.GroupId,
                        request.VendorId,
                        request.Limit,
                        request.Page,
                        request.OrderBy
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;

                    var reponse = new GroupEmployeeHaveNotInGroupRequestReponse()
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

        public async Task<GroupEmployeeReponse> GetALl(GroupEmployeeRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<GroupEmployeeIndexModel>(_Sql.GroupEmpl_getAll, new
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
                    var reponse = new GroupEmployeeReponse()
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

        public async Task<int> UpdateAsyn(GroupEmployee entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {

                nameof(entity.UpdateAt),

                nameof(entity.CreateAt),
                nameof(entity.CreatedBy),
                nameof(entity.Deleted)
            });
            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.GroupEmpl_update, par, commandType: CommandType.StoredProcedure);

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
