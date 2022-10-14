namespace VS.Core.Repository.baseConfig
{
    public sealed class SqlContraint

    {
        private SqlContraint() { }

        private static SqlContraint _sqlContraint;

        public static SqlContraint GetVariable()
        {
            if (_sqlContraint == null)
            {
                _sqlContraint = new SqlContraint();
            }
            return _sqlContraint;
        }

        public string Employee_login = "sp_Employee_Login";

        public string Employee_insert = "sp_Employee_Insert";

        public string Employee_update = "sp_Employee_Update";

        public string Employee_getAll = "sp_Employee_getAll";

        public string Employee_exportData = "sp_Employee_exportData";



        public string MasterData_reason_insert = "sp_reason_Insert";

        public string MasterData_reason_update = "sp_reason_Update";

        public string MasterData_reason_getAll = "sp_reason_getAll";

        public string MasterData_reason_exportData = "sp_Employee_exportData";


        public string Group_reason_insert = "sp_groupReason_Insert";

        public string Group_reason_update = "sp_groupReason_Update";

        public string Group_reason_getAll = "sp_groupReason_getAll";

        public string Group_reason_exportData = "sp_groupReason_exportData";




        public string Campaign_insert = "sp_Campaign_Insert";

        public string Campaign_update = "sp_Campaign_update";

        public string Campaign_getAll = "sp_campaign_getAll";

        public string Campaign_exportData = "sp_Employee_exportData";






    }
}
