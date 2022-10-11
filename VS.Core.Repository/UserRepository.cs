using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;

namespace VS.Core.Repository
{
    public class UserRepository : RepositoryBase<Account>, IUserRepository
    {

        private string _tableName = "";
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<Account> GetById(string Id)
        {
            throw new NotImplementedException();

        }

        public Task<bool> InsertOrUpdate(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> Login(string userName, string password)
        {
            using (var con = GetConnection())
            {
                var result = await con.QueryFirstOrDefaultAsync<Account>(_Sql.Employee_login, new
                {
                    userName,
                    password
                }, commandType: CommandType.StoredProcedure);
                return result;
            }

        }

    }
}
