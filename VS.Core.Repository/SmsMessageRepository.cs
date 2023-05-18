using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Repository
{
    public class SmsMessageRepository : RepositoryBase<SmsMessage>, ISmsMessageRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "SmsMessage";
        public SmsMessageRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public async Task<int> Add(SmsMessage entity)
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
                    var result = await _con.ExecuteAsync(_Sql.Sms_insert, par, commandType: CommandType.StoredProcedure);

                    return 1;
                }
            }
            catch (Exception e)
            {
                return 0;

            }

        }

        public Task<int> Update(SmsMessage entity)
        {
            throw new NotImplementedException();
        }
    }
}
