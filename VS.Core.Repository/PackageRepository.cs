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
    public class PackageRepository : RepositoryBase<Package>, IPackageRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Package";
        public PackageRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(Package entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            var par = GetParams(entity,
            new string[] {
                nameof(entity.UpdatedBy),
                nameof(entity.UpdateAt),
                nameof(entity.Id),
                nameof(entity.CreateAt),
                nameof(entity.Deleted)
            }
            , "Id");

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Package_Insert,
                        new
                        {
                            entity.Value,
                            entity.VendorId,
                            entity.Name,
                            entity.Type,
                            entity.IdUser,
                            entity.From,
                            entity.Priorities,
                            entity.To,
                            entity.CreatedBy,
                            entity.Status
                        },
                        commandType: CommandType.StoredProcedure);
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
                var result = await con.QuerySingleOrDefaultAsync<Package>(sql, new { Code = code });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }



        public async Task<GetCountBYMinMaxReponse> GetCountBYMinMax(GetCountBYMinMaxRequest request)
        {
            var reponse = new GetCountBYMinMaxReponse()
            {
                Total = 0

            };

            try
            {


                using (var con = GetConnection())
                {


                    var result = await con.QueryFirstAsync<GetCountBYMinMaxIndexModel>("sp_Package_getCounted", new
                    {
                        request.VendorId,
                        request.Min,
                        request.Max,
                        request.UserId
                    }, commandType: CommandType.StoredProcedure);

                    if (result == null)
                    {
                        return reponse;
                    };

                    reponse.Total = result.Total;
                    reponse.Remain = result.Remain;
                    return reponse;
                }
            }
            catch (Exception e)
            {
                return reponse;
            }
        }

        public async Task<PackageReponse> GetALl(PackageRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {

                    int status = -1;
                    try
                    {
                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            status = int.Parse(request.Status);
                        }
                    }
                    catch (Exception)
                    {

                        status = -1;
                    }
                    var result = await con.QueryAsync<PackageIndexModel>(_Sql.Package_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.VendorId,



                        status,

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
                    var reponse = new PackageReponse()
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

        public async Task<PackageReponse> GetALlInfo(PackageRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {

                    int status = -1;
                    try
                    {
                        if (!string.IsNullOrEmpty(request.Status))
                        {
                            status = int.Parse(request.Status);
                        }
                    }
                    catch (Exception)
                    {

                        status = -1;
                    }
                    var result = await con.QueryAsync<PackageIndexModel>(_Sql.Package_getAll2, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.VendorId,
                        request.UserId,
                        status,

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
                    var reponse = new PackageReponse()
                    {
                        Total = totalRecord,
                        Data = result
                    };
                    return reponse;
                }
            }
            catch (Exception e)
            {
                var reponse = new PackageReponse()
                {
                    Total = 0,
                    Data = new List<PackageIndexModel>()
                };
                return reponse;
            }
        }




        public async Task<int> Update(Package entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(
                    _Sql.Package_Update, new
                    {
                        entity.Name,
                        entity.Id,
                        entity.From,
                        entity.Type,
                        entity.To,
                        entity.Priorities,
                        entity.IdUser,
                        entity.Value,
                        entity.Status

                    }, commandType: CommandType.StoredProcedure);

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
