using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class DpdRepository : RepositoryBase<Dpd>, IDpdRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "DPD";
        public DpdRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(Dpd entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Dpd_Insert,
                        new
                        {
                            entity.Max,
                            entity.Min,
                            entity.Name,
                            entity.VendorId,
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
                var result = await con.QuerySingleOrDefaultAsync<GroupReason>(sql, new { Code = code });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }


        public async Task<DpdReponse> GetALl(DPDRequest request)
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
                    var result = await con.QueryAsync<DpdIndexModel>(_Sql.Dpd_getAll, new
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
                    var reponse = new DpdReponse()
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

        public async Task<DpdReponse> GetInfoPackage(DPDRequest request)
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
                    var result = await con.QueryAsync<DpdIndexModel>(_Sql.Dpd_getAll, new
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
                    var reponse = new DpdReponse()
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

        public async Task<int> Update(Dpd entity)
        {
            entity.CreateAt = DateTime.Now;
            entity.UpdateAt = DateTime.Now;

            try
            {
                using (var _con = GetConnection())
                {
                    var result = await _con.ExecuteAsync(_Sql.Dpd_Update, new
                    {
                        entity.Name,
                        entity.Id,
                        entity.Min,
                        entity.Max,
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
