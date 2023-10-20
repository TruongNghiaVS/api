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

        public string Employee_insert = "sp_Employee_Insertv2";

        public string Employee_update = "sp_Employee_Update";
        public string Employee_delete = "sp_Employee_Delete";
        public string Employee_getAll = "sp_Employee_getAll";

        public string Employee_exportData = "sp_Employee_exportData";

        public string MasterData_reason_insert = "sp_reason_Insert";

        public string MasterData_reason_update = "sp_reason_Update";

        public string MasterData_reason_getAll = "sp_reason_getAll";

        public string MasterData_reason_exportData = "sp_Employee_exportData";


        public string Line_Insert = "sp_line_Insert";
        public string Line_Update = "sp_line_Update";
        public string Line_getAll = "sp_line_getAll2";



        public string Dpd_Insert = "sp_DPD_Insert";
        public string Dpd_Update = "sp_DPD_Update";
        public string Dpd_getAll = "sp_dpd_getAll";
        public string Dpd_getInfo = "sp_dpd_getInfo";

        public string Package_Insert = "sp_Package_Insert";
        public string Package_Update = "sp_Package_Update";
        public string Package_getAll = "sp_Package_getAll";

        public string Package_getAll2 = "sp_Package_getAll2";

        public string Group_reason_insert = "sp_groupReason_Insert";

        public string Group_reason_update = "sp_groupReason_Update";

        public string ImpactHistoryFinal_getAll = "sp_ImpactHistoryFinal_getAll";

        public string Group_reason_getAll = "sp_Reason_getAll";

        public string Sp_reason_getAllByHistoryReport = "sp_reason_getAllByHistoryReport";


        public string Group_sp_Reason_getAll = "sp_groupReason_getAll";

        public string Group_reason_exportData = "sp_groupReason_exportData";
        public string campaign_UpdateOverview = "sp_campaign_UpdateOverview";
        public string Campaign_insert = "sp_Campaign_Insert";

        public string Campaign_update = "sp_Campaign_update";

        public string Campaign_update_caseSkip = "sp_CampaignProfile_updateCaseSkip";

        public string Campaign_getOverviewbyId = "sp_campaign_getOverviewbyId";

        public string Campaign_getAll = "sp_campaign_getAll";
        public string Campaign_sp_campaign_getOverviewDashboardbyId = "sp_campaign_getOverviewDashboardbyId";
        public string Campaign_GetAllAsiggeeByCampagnId = "sp_campaign_getAll_AssigneebyCampagnId";

        public string Campaign_exportData = "sp_Employee_exportData";


        public string CampaignProfile_insert = "sp_CampaignProfile_Insert";

        public string CampaignProfile_update = "sp_CampaignProfile_update";
        public string CampaignProfile_update_skip = "sp_CampaignProfile_update_skip";
        public string Sp_CampaignProfile_import_update = "sp_CampaignProfile_import_update";
        public string Sp_CampaignProfile_resetCase = "sp_CampaignProfile_resetCase";
        public string CampaignProfile_getAll = "sp_CampaignProfile_getAll";
        public string CampaignProfile_getAllNoted = "sp_campagnProfileGetAllNoted";
        public string CampaignProfile_exportData = "sp_CampaignProfile_ExportData";

        public string CampaignImpact_insert = "sp_ImpactHistory_Insert";
        public string SkipInfo_insert = "sp_SkipInfo_Insert";

        public string SkipInfo_getAll = "sp_SkipInfo_getAll";
        public string Sp_ImpactHistory_Insert2 = "sp_ImpactHistory_Insert2";

        public string CampaignImpact_update = "CampaignImpact_update";

        public string CampaignImpact_getAll = "sp_ImpactHistory_getAll";


        public string CampaignImpact_getAllv2 = "sp_ImpactHistory_getAllv2";

        public string CampaignImpact_getAllHistory = "sp_ImpactHistory_getAllHistory";
        public string CallLog_getAll = "sp_CallLog_getAll";
        public string CallLog_insert = "sp_CallLog_Insert";
        public string LogCall_insert = "sp_LogCall_Insert";
        public string ViewRecording_insert = "sp_ViewRecording_Insert";
        public string Sms_insert = "sp_Sms_Insert";
        public string Sms_insert2 = "sp_Sms_Insert2";

        public string Sms_update2 = "sp_Sms_Update2";

        public string Sms_CheckExitsCall = "sp_CheckExitsCall";
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


        public string ReportTalkTime_insert = "sp_ReportTalkTime_Insert2";
        public string ReportTalkTime_update = "sp_ReportTalkTime_Update";
        public string ReportTalkTime_getAll = "sp_ReportTalkTime_getAll";
        public string ReportgetAllCall = "sp_ReportgetAllCall";
        public string sms_getAll = "sp_sms_getAll";
        public string View_Recording = "sp_view_Recording";
        public string Data_Qc = "sp_qc_GetData";
        public string WorkplaceNoted_Insert = "sp_WorkplaceNoted_Insert";
        public string sms_getAllHandle = "sp_sms_getAllHandle";
        public string ReportTalkTime_getAllNotDeleteFile = "sp_ReportTalkTime_getAllNotDeleteFile";
        public string RecordingFile_getAll = "sp_RecordingFile_getAll";
        public string RecordingFile_getAllv3 = "sp_RecordingFile_getAllv3";
        public string Call_FirstLast = "sp_RecordingFile_fetFLCall";


        public string RecordingFile_getAllWithNo = "sp_RecordingFile_getAllWithNo";
        public string RecordingFileExport_getAll = "sp_RecordingFileExport_getAll";
        public string RecordingFileExportNo_getAll = "sp_RecordingFileExportNo_getAll";



        public string GetAllRecordGroupByLineCode_getAll = "sp_GetAllRecordGroupByLineCode_getAll";

        public string GetAllRecordGroupByLineCodeExport_getAll = "sp_GetAllRecordGroupByLineCodeExport_getAll";

        public string Sp_ProcessingCalTimeIndexModel_getAll = "sp_ProcessingCalTimeIndexModel_getAll";

        public string GetGetOverViewDashBoard_getAll = "sp_GetGetOverViewDashBoard_getAll";


    }
}
