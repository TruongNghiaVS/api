using Newtonsoft.Json;
using System.Net.Http.Headers;
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
            timerun = timerun.AddMinutes(-15);
            var startTime = timerun;
            var endTime = DateTime.Now.EndDateTime();
            Task.WaitAll();
            if (dateGet == null)
            {
                dateGet = DateTime.UtcNow;
            }
            var i = 0;

            while (i < 4)
            {
                IEnumerable<ReportQuerryTaltimeIndex> allcdrHaving;
                if (i == 0)
                {
                    i++;
                    continue;

                    startTime = startTime.AddMinutes(-15);
                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe4(
                        new core.Request.HandlelFileRecordingRequest()
                        {
                            TimeSelect = dateGet,
                            TimeFrom = startTime,
                            TimeTo = endTime
                        }
                    );
                }
                else if (i == 1)
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
                else if (i == 2)
                {
                    i++;
                    continue;
                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe3(
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
                    allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
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

                    var resultInsert = await _unitOfWork1.ReportTalkTimeRepository.Add(reportTalkTime);

                }
                Task.WaitAll();
            }

            Task.WaitAll();
            return await Task.FromResult(0);


        }

        public async Task<int> DeleteFileRecoring(bool? DeleteAll = false)
        {
            var index = 1;
            var listLine19 = new List<string>();
            var listLine12 = new List<string>();

            var allRecored = await _unitOfWork1.ReportTalkTimeRepository.GetAllDeleted();
            index = allRecored.Count;

            if (index > 1)
            {
                foreach (var item in allRecored)
                {
                    if (item.LineCode.StartsWith("1"))
                    {
                        listLine19.Add(item.FileRecording);
                    }
                    else
                    {

                        listLine12.Add(item.FileRecording);
                    }
                }
            }




            foreach (var item in listLine19)
            {


                var linkUrl = "http://192.168.1.10:3002";


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(linkUrl);
                    var data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        filePath = item
                    }));
                    data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var reponse = await client.PostAsync("api/deleteFile", data);
                    var result = await reponse.Content.ReadAsStringAsync();
                    if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await _unitOfWork1.ReportTalkTimeRepository.UpdateFileDeleted(item);

                    }
                }
            }

            foreach (var item in listLine12)
            {



                var linkUrl = "http://192.168.1.12:3002";


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(linkUrl);
                    var data = new StringContent(JsonConvert.SerializeObject(new
                    {
                        filePath = item
                    }));
                    data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var reponse = await client.PostAsync("api/deleteFile", data);
                    var result = await reponse.Content.ReadAsStringAsync();
                    if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        await _unitOfWork1.ReportTalkTimeRepository.UpdateFileDeleted(item);

                    }
                }
            }

            return 1;
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

                    await _unitOfWork1.ReportTalkTimeRepository.Add(reportTalkTime);
                }
                Task.WaitAll();
            }

            Task.WaitAll();
            return await Task.FromResult(0);


        }

    }
}
