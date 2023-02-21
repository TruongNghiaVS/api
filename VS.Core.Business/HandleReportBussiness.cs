using VS.core.Utilities;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class HandleReportBussiness : IHandleReportBussiness
    {

        private readonly IUnitOfWork _unitOfWork1;
        public HandleReportBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public async Task<int> CalTalkingTime(DateTime? dateGet)
        {
            if (dateGet == null)
            {
                dateGet = DateTime.UtcNow;
            }

            await _unitOfWork1.ReportTalkTimeRepository.DeleteAll();

            var i = 0;

            while (i < 2)
            {
                IEnumerable<ReportQuerryTaltimeIndex> allcdrHaving;

                if (i == 0)
                {
                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
                    new core.Request.HandlelFileRecordingRequest()
                    {
                        TimeSelect = dateGet
                    }
                    );
                }
                else
                {
                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe2(
                    new core.Request.HandlelFileRecordingRequest()
                    {
                        TimeSelect = dateGet
                    }
                    );
                }
                i++;
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
                Task.WaitAll();
            }

            Task.WaitAll();
            return await Task.FromResult(0);


        }
    }
}
