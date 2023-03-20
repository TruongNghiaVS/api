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
            var timerun = DateTime.UtcNow;
            timerun = timerun.AddHours(-1);
            var startTime = timerun;
            var endTime = DateTime.Now;
            //await _unitOfWork1.ReportTalkTimeRepository.DeleteAllRangeFromTo(startTime, endTime);
            Task.WaitAll();
            if (dateGet == null)
            {
                dateGet = DateTime.UtcNow;
            }
            var i = 0;

            while (i < 1)
            {
                IEnumerable<ReportQuerryTaltimeIndex> allcdrHaving;
                if (i == 0)
                {

                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
                        new core.Request.HandlelFileRecordingRequest()
                        {
                            TimeSelect = dateGet,
                            TimeFrom = startTime,
                            TimeTo = endTime
                        }
                    );
                }
                else
                {


                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe2(
                        new core.Request.HandlelFileRecordingRequest()
                        {
                            TimeSelect = dateGet,
                            TimeFrom = startTime,
                            TimeTo = endTime

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
                    var usergetByLinecode = await _unitOfWork1.Employees.GetByLineCode(item.LineCode);
                    var reportTalkTime = new ReportTalkTime()
                    {
                        CallDate = item.CallDate,
                        Sourcecall = item.SourceCall,
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
                    if (usergetByLinecode != null)
                    {

                        reportTalkTime.VendorId = usergetByLinecode.VendorId;
                    }

                    await _unitOfWork1.ReportTalkTimeRepository.AddAsync(reportTalkTime);
                }
                Task.WaitAll();
            }

            Task.WaitAll();
            return await Task.FromResult(0);


        }

        public async Task<int> CalTalkingTimeAll(DateTime? dateGet)
        {
            var timerun = DateTime.UtcNow.AddDays(-5);


            var startTime = new DateTime(timerun.Year, timerun.Month, timerun.Day, timerun.Hour, 0, 0);


            var endTime = startTime.AddHours(1).AddSeconds(-1);
            //startTime = startTime.AddSeconds(-900);

            //startTime = DateTime.Now.AddDays(1);

            endTime = DateTime.Now.AddDays(1).AddSeconds(-1);

            await _unitOfWork1.ReportTalkTimeRepository.DeleteAllRangeFromTo(startTime, endTime);
            Task.WaitAll();
            if (dateGet == null)
            {
                dateGet = DateTime.UtcNow;
            }
            var i = 0;

            while (i < 2)
            {
                IEnumerable<ReportQuerryTaltimeIndex> allcdrHaving;
                if (i == 0)
                {

                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
                        new core.Request.HandlelFileRecordingRequest()
                        {
                            TimeSelect = dateGet,
                            TimeFrom = startTime,
                            TimeTo = endTime
                        }
                    );
                }
                else
                {


                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe2(
                        new core.Request.HandlelFileRecordingRequest()
                        {
                            TimeSelect = dateGet,
                            TimeFrom = startTime,
                            TimeTo = endTime

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
                    var usergetByLinecode = await _unitOfWork1.Employees.GetByLineCode(item.LineCode);
                    var reportTalkTime = new ReportTalkTime()
                    {
                        CallDate = item.CallDate,
                        Sourcecall = item.SourceCall,
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
                    if (usergetByLinecode != null)
                    {

                        reportTalkTime.VendorId = usergetByLinecode.VendorId;
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
