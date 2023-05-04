using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class EmployeeRepository : RepositoryBase<Account>, IEmployeeRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Employees";
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> AddAsync(Account entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                nameof(entity.Permissions),
                nameof(entity.OrgId),
                nameof(entity.UpdatedBy),
                nameof(entity.UpdateAt),
                nameof(entity.Id),
                nameof(entity.PhoneNumber),
                nameof(entity.CreateAt),
                nameof(entity.Deleted)
            }, "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Employee_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }


        public async Task<Account> GetByLineCode(string lineCode)
        {
            using (var con = GetConnection())
            {
                var sql = "select top 1  * from Employees d where d.LineCode = @LineCode  ";
                var result = await con.QuerySingleOrDefaultAsync<Account>(sql, new { LineCode = lineCode });
                return result;
            }
        }


        public async Task<bool> CheckDuplicate(string userName, string phone)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE UserName = @userName";
                var result = await con.QuerySingleOrDefaultAsync<Account>(sql, new { UserName = userName });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }


        public async Task<bool> UpdatePass(string userId, string pass)
        {
            using (var con = GetConnection())
            {

                var result = await con.QuerySingleOrDefaultAsync<Account>("sp_Employee_changePass",
                    new
                    {

                        pass = pass,
                        id = userId

                    }, commandType: CommandType.StoredProcedure);

                return true;
            }
        }


        public async Task<EmployeeSearchReponse> GetALl(EmployeeSearchRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<EmployeeIndexModel>(_Sql.Employee_getAll, new
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
                    var reponse = new EmployeeSearchReponse()
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


        public async Task<EmployeeSearchReponse> GetDataForExport(EmployeeSearchRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<EmployeeIndexModel>(_Sql.Employee_exportData, new
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
                    var reponse = new EmployeeSearchReponse()
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

        public async Task<int> UpdateAsyn(Account entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity, new string[] {
                 nameof(entity.Permissions),
                nameof(entity.OrgId),
                nameof(entity.UpdateAt),
                nameof(entity.CreateAt),
                 nameof(entity.PhoneNumber),
                nameof(entity.Deleted),
                nameof(entity.CreatedBy)
            });

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Employee_update, par, commandType: CommandType.StoredProcedure);

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
