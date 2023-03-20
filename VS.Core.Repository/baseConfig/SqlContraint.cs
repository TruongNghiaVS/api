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


        public string Line_Insert = "sp_line_Insert";
        public string Line_Update = "sp_line_Update";
        public string Line_getAll = "sp_line_getAll";


        public string Group_reason_insert = "sp_groupReason_Insert";

        public string Group_reason_update = "sp_groupReason_Update";

        public string Group_reason_getAll = "sp_groupReason_getAll";

        public string Group_reason_exportData = "sp_groupReason_exportData";
        public string campaign_UpdateOverview = "sp_campaign_UpdateOverview";
        public string Campaign_insert = "sp_Campaign_Insert";

        public string Campaign_update = "sp_Campaign_update";

        public string Campaign_getOverviewbyId = "sp_campaign_getOverviewbyId";

        public string Campaign_getAll = "sp_campaign_getAll";
        public string Campaign_GetAllAsiggeeByCampagnId = "sp_campaign_getAll_AssigneebyCampagnId";

        public string Campaign_exportData = "sp_Employee_exportData";


        public string CampaignProfile_insert = "sp_CampaignProfile_Insert";

        public string CampaignProfile_update = "sp_CampaignProfile_update";
        public string Sp_CampaignProfile_resetCase = "sp_CampaignProfile_resetCase";
        public string CampaignProfile_getAll = "sp_CampaignProfile_getAll";

        public string CampaignImpact_insert = "sp_ImpactHistory_Insert";

        public string CampaignImpact_update = "CampaignImpact_update";

        public string CampaignImpact_getAll = "sp_ImpactHistory_getAll";
        public string CallLog_getAll = "sp_CallLog_getAll";
        public string CallLog_insert = "sp_CallLog_Insert";
        public string LogCall_insert = "sp_LogCall_Insert";
        public string ReportTalkTimeGroupByDay_insert = "sp_reportTalkTimeGroupByDay_insert";
        public string ReportTalkTimeGroupByDay_update = "sp_reportTalkTimeGroupByDay_update";

        public string CampaignProfile_handleCase = "sp_CampaignProfile_case";

        public string MasterData_reasonNew_insert = "sp_MasterData_insert";

        public string MasterData_reasonNew_update = "sp_MasterData_update";

        public string MasterData_reasonNew_getAll = "sp_MasterData_getAll";

        public string GroupEmpl_insert = "sp_GroupEmpl_Insert";
        public string sp_GroupMember_Insert = "sp_GroupMember_Insert";

        public string GroupEmpl_update = "sp_GroupEmpl_Update";

        public string GroupEmpl_getAll = "sp_GroupEmpl_getAll";

        public string Sp_GetAllMeberHaveNotGroup = "sp_GetAllMeberHaveNotGroup";
        public string sp_GetAllMeberByGroupId = "sp_GetAllMeberByGroupId";
        public string GroupEmpl_getAllManager = "sp_GroupEmpl_getAllManager";

        public string LoginReport_insert = "sp_LogHistory_Insert";
        public string LoginReport_update = "sp_LogHistory_update";
        public string LoginReport_getAll = "sp_LogHistory_getAll";


        public string ReportImpactOverview_GetAll = "ReportImpactOverview_GetAll";
        public string ReportImpact_GetAll = "ReportImpact_GetAll";

        public string ExportImpactData_GetAll = "ExportImpactData_GetAll";

        public string ReportImpact_GetAllRecordingFile = "ReportImpact_GetAllRecordingFile";
        public string ReportCampaignProfile = "sp_CampaignProfile_getOverviewDashboard";


        public string ReportTalkTime_insert = "sp_ReportTalkTime_Insert";
        public string ReportTalkTime_update = "sp_ReportTalkTime_Update";
        public string ReportTalkTime_getAll = "sp_ReportTalkTime_getAll";
        public string RecordingFile_getAll = "sp_RecordingFile_getAll";
        public string RecordingFileExport_getAll = "sp_RecordingFileExport_getAll";

        public string GetAllRecordGroupByLineCode_getAll = "sp_GetAllRecordGroupByLineCode_getAll";

        public string GetAllRecordGroupByLineCodeExport_getAll = "sp_GetAllRecordGroupByLineCodeExport_getAll";

        public string Sp_ProcessingCalTimeIndexModel_getAll = "sp_ProcessingCalTimeIndexModel_getAll";

        public string GetGetOverViewDashBoard_getAll = "sp_GetGetOverViewDashBoard_getAll";


    }
}
