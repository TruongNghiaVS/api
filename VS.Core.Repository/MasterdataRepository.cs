﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class MasterdataRepository : RepositoryBase<MasterData>, IMasterDataRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "Reason";
        public MasterdataRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(MasterData entity)
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
                    var result = await _con.ExecuteAsync(_Sql.MasterData_reason_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }



        public async Task<bool> CheckDuplicate(string code, string vendorId = null)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE code = @Code";
                if (!string.IsNullOrEmpty(vendorId))
                {
                    sql = "SELECT * FROM " + _baseTable + " WHERE code = @Code and VendorId = @VendorId ";
                }
                var result = await con.QuerySingleOrDefaultAsync<Account>(sql, new { Code = code, VendorId = vendorId });

                if (result == null)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<MasterData> GetbyCode(string code, string vendorId = null)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE code = @Code";
                if (!string.IsNullOrEmpty(vendorId))
                {
                    sql = "SELECT * FROM " + _baseTable + " WHERE code = @Code and VendorId = @VendorId ";
                }
                var result = await con.QuerySingleOrDefaultAsync<MasterData>(sql, new { Code = code, VendorId = vendorId });
                return result;
            }
        }



        public async Task<MasterDataReponse> GetALl(MaterDataRequest request)
        {
            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<MasterDataIndexModel>(_Sql.MasterData_reason_getAll, new
                    {
                        request.Token,
                        request.From,
                        request.To,
                        request.Limit,
                        request.Page,
                        request.OrderBy,
                        request.GroupStatus
                    }, commandType: CommandType.StoredProcedure);

                    var fistElement = result.FirstOrDefault();
                    var totalRecord = 0;
                    if (fistElement != null)
                    {
                        totalRecord = fistElement.TotalRecord;
                    }
                    var reponse = new MasterDataReponse()
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

        public async Task<MasterDataReponse> GetDataForExport(MaterDataRequest request)
        {

            int page = request.Page;
            int limit = request.Limit;

            ProcessInputPaging(ref page, ref limit, out offset);
            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<MasterDataIndexModel>(_Sql.MasterData_reason_getAll, new
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
                    var reponse = new MasterDataReponse()
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

        public async Task<int> Update(MasterData entity)
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
                    var result = await _con.ExecuteAsync(_Sql.MasterData_reason_update, par, commandType: CommandType.StoredProcedure);

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
