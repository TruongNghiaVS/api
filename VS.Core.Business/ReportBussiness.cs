﻿using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class ReportBussiness : IReportBussiness
    {

        private readonly IUnitOfWork _unitOfWork1;
        public ReportBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public Task<ReportImpactReponse> GetAllReportImapact(ReportImpactRequest request)
        {
            return _unitOfWork1.ReportRe.GetAllReportImapact(request);
        }
        public Task<ImpactDashboardOverviewReponse> ReportImpactDashboardOverview(ReportImpactRequest request)
        {
            return _unitOfWork1.ReportRe.GetALlOverView(request);
        }
        public Task<ReportCDRReponse> GetAllReportCDR(ReportCDRequest request)
        {
            return _unitOfWork1.ReportRe.GetAllReportCDR(request);
        }
        public Task<ReportCDRReponse> GetAllRecordingFile(ReportCDRequest request)
        {
            return _unitOfWork1.ReportRe.GetAllRecordingFile(request);
        }
        public Task<ReportCDRReponse> GetAllReportRecordingFile(ReportCDRequest request)
        {
            return _unitOfWork1.ReportRe.GetAllReportRecordingFile(request);
        }
        public Task<int> GetReportOverviewAgrree(GetReportOverviewAgrreeRequest request)
        {
            return _unitOfWork1.ReportRe.GetReportOverviewAgrree(request);
        }
        public Task<GetOverViewInfoReponse> GetOverViewInfo(GetOverViewInfoRequest request)
        {
            return _unitOfWork1.ReportRe.GetOverViewInfo(request);
        }
        public Task<GetOverViewTalkingItemReponse> GetOverViewTimeTalking(GetOverViewInfoRequest request)
        {
            return _unitOfWork1.ReportRe.GetOverViewTimeTalking(request);
        }
        public async Task<int> CalTalkingTime()

        {
            return await Task.FromResult(0);
        }
    }
}