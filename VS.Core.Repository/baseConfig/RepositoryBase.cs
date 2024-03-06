using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using VS.Core.dataEntry;

namespace VS.Core.Repository.baseConfig
{
    public abstract class RepositoryBase<T> where T : class, ITypeWithId

    {
        private IDbConnection _connection;
        protected readonly IConfiguration _configuration;
        protected int offset;

        protected SqlContraint _Sql;
        protected string _baseTable;
        public RepositoryBase(IConfiguration configuration, string baseTable = "")
        {
            _configuration = configuration;
            _connection = new SqlConnection(configuration.GetConnectionString("stringConnect"));
            _baseTable = baseTable;
            _Sql = SqlContraint.GetVariable();
        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection(_configuration.GetConnectionString("stringConnect"));
            con.Open();
            return con;
        }
        protected IDbConnection StoreConnection()
        {
            var con = new SqlConnection(_configuration.GetConnectionString("stringConnect7"));
            con.Open();
            return con;
        }
        protected DynamicParameters AddOutputParam(string name, DbType type = DbType.Int32)
        {
            var p = new DynamicParameters();
            p.Add(name, dbType: type, direction: ParameterDirection.Output);
            return p;
        }



        protected DynamicParameters GetParams<TPar>(TPar model, string[] ignoreKey = null, string outputParam = "", DbType type = DbType.Int32)
        {
            var p = new DynamicParameters();
            if (!string.IsNullOrWhiteSpace(outputParam))
                p.Add(outputParam, dbType: type, direction: ParameterDirection.Output);
            var properties = model.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var key = prop.Name;
                var value = prop.GetValue(model);
                if (ignoreKey != null && ignoreKey.Contains(key))
                    continue;
                if (!string.IsNullOrWhiteSpace(outputParam))
                {
                    if (key.ToLower() == outputParam.ToLower())
                    {
                        continue;
                    }
                }
                if (!string.IsNullOrWhiteSpace(key))
                    p.Add(key, value);
            }
            return p;
        }
        protected void ProcessInputPaging(ref int page, ref int limit, out int offset)
        {
            page = page <= 0 ? 1 : page;
            if (limit <= 0)
                limit = 20;
            if (limit > 1000)
                limit = 100;
            offset = (page - 1) * limit;
        }
        protected string GetException(Exception e)
        {
            if (e == null)
                return string.Empty;
            return e.InnerException == null ? e.Message : e.InnerException.Message;


        }

        public Task Delete(T entity)
        {
            using (var con = GetConnection())
            {
                con.Execute("update " + _baseTable + " set  Deleted =1  WHERE Id = @ID", new { ID = entity.Id });

            }


            return Task.CompletedTask;
        }

        public async Task<T> GetById(string id)
        {
            using (var con = GetConnection())
            {
                var sql = "SELECT * FROM " + _baseTable + " WHERE Id = @id";
                var result = await con.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
                return result;
            }
        }

    }
}
