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
            _connection = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=vsrolapi;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
        }
        protected IDbConnection GetConnection()
        {
            var con = new SqlConnection("Server=192.168.1.3,1433; Initial Catalog=vsrolapi;User ID=crm;Password=Vietstar@2018; Persist Security Info=False;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;Integrated Security=false;");
            con.Open();
            return con;
        }


        public async Task<List<PhoneLog>> GetAllData()
        {

            try
            {
                using (var con = GetConnection())
                {
                    var result = await con.QueryAsync<PhoneLog>("select id, NoAgreement, MobilePhone from CampaignProfile where CampaignId = 1058", new { }, commandType: CommandType.Text);

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
