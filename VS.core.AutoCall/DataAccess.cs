using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace VS.core.AutoCall
{
    public class DataAccess

    {
        private IDbConnection _connection;

        public DataAccess()
        {
            _connection = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=crm2025;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=crm2025;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
            con.Open();
            return con;
        }

        public async Task<bool> AddLogCall(string phonenumber, int noId, string line)
        {

            using (var con = GetConnection())
            {
                try
                {
                    var result = await con.ExecuteAsync("sp_LogAutocall_Insert", new
                    {
                        phoneNumber = phonenumber,
                        NoId = noId,
                        line = line,
                        callat = DateTime.Now
                    }, commandType: CommandType.StoredProcedure);
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }

        }

        public async Task<List<PhoneLog>> GetAllData(bool loadDatasip = false)
        {
            try
            {
                using (var con = GetConnection())
                {
                    var sqlText = "select id,CustomerName, NoAgreement, MobilePhone, dbo.getLineCode(Assignee )  as lineCode from CampaignProfile where CampaignId = 4 and isnull( numberCall,0) < 1 order by id desc ";
                    if (loadDatasip == true)
                    {
                        sqlText = "select id,CustomerName, NoAgreement, MobilePhone, dbo.getLineCode(Assignee )  as lineCode from CampaignProfile d where CampaignId = 2  and isnull(d.status, 0) not in (0,1,10,20) order by id desc ";
                    }
                    var result = await con.QueryAsync<PhoneLog>(sqlText, new { }, commandType: CommandType.Text);
                    if (result == null)
                    {
                        return new List<PhoneLog>();
                    }
                    return result.ToList();
                }
            }
            catch (Exception e)
            {
                return new List<PhoneLog>();
            }
        }
        protected IDbConnection GetMysqlConnection()
        {
            var con = new MySqlConnection("Server=192.168.1.151;uid=demo1;Pwd=123456789;database=asteriskcdrdb;");
            con.Open();
            return con;
        }

    }
}
