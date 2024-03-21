using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using Org.BouncyCastle.Security;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using Org.BouncyCastle.Asn1.Ocsp;
using VS.Core.Repository.Model;

namespace VS.Core.Repository
{
    public class AutoCallRepository : RepositoryBase<LogCall>, IAutoRepository
    {

        private readonly IConfiguration _configuration;

        private readonly string tableName = "LogCall";
        public AutoCallRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _baseTable = tableName;
        }

        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }
      
        public async Task<bool> HandleDataAutoCall()
        {
            var dataCal = new List<ReportQuerryTaltimeIndex>();
          
            using (var con = GetConnectionAutoCall())
            {
                var sqlQuerry = "SELECT d.cnum AS 'LineCode', '1' as SourceCall ,  d.dst AS  'PhoneLog', d.linkedid AS 'Linkedid', d.calldate,  d.disposition, d.billsec AS 'DurationBill', d.duration AS 'Duration', d.recordingfile AS 'FileRecording' FROM cdr d WHERE  d.lastapp = 'Dial' AND d.calldate >= @timeFrom and d.calldate <= @timeTo and d.lastapp = 'Dial' ";
                var result = await con.QueryAsync<ReportQuerryTaltimeIndex>(sqlQuerry, new
                {
                    
                });
                foreach (var item in result)
                {
                    if(item.Disposition == "ANSWERED" ||  
                        item.Disposition =="" )
                    {
                         

                    }

                    else
                    {

                    }

                }
            }
            return true;
        }

        public Task<int> Update(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        Task<CampagnProfile> IGenericRepository<CampagnProfile>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
