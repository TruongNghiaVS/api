using VS.core.Utilities;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class HandleReportBussiness : IHandleReportBussiness
    {

        private readonly IUnitOfWork _unitOfWork1;
        public HandleReportBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }


        public async Task<int> CalTalkingTime()
        {

            await _unitOfWork1.ReportTalkTimeRepository.DeleteAll();
            var allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
                new core.Request.HandlelFileRecordingRequest()
                {

                }
            );

            var data = allcdrHaving;

            if (data == null)
            {
                return 0;
            }

            foreach (var item in data)
            {
                var reportTalkTime = new ReportTalkTime()
                {
                    CallDate = item.CallDate,
                    LineCode = item.LineCode,
                    PhoneLog = item.PhoneLog,
                    Linkedid = item.Linkedid,
                    Disposition = item.Disposition,
                    DurationBill = item.DurationBill
                };
                reportTalkTime.Duration = item.Duration;
                if (item.Disposition == "ANSWERED" && !string.IsNullOrWhiteSpace(item.FileRecording))
                {
                    reportTalkTime.FileRecording = Utils.GetFileRecordingFile(item.FileRecording, item.CallDate);
                    reportTalkTime.DurationReal = reportTalkTime.FileRecording.GetDurationAudio();
                }
                await _unitOfWork1.ReportTalkTimeRepository.AddAsync(reportTalkTime);
            }
            return await Task.FromResult(0);


        }
    }
}
