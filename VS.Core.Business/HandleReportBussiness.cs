using VS.core.Utilities;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class LivePhone
    {
        public string LineCall { get; set; }
        public string Phone { get; set; }
        public string NoAgree { get; set; }

        public int StatusCheck { get; set; }

        public bool IsLive { get; set; }
    }

    public class HandleReportBussiness : IHandleReportBussiness
    {

        private readonly IUnitOfWork _unitOfWork1;
        private List<LivePhone> DataCheck;
        public HandleReportBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
            DataCheck = new List<LivePhone>();
        }
        public async Task<int> CalTalkingTime(DateTime? dateGet)
        {
            var timerun = DateTime.Now;
            if (dateGet.HasValue)
            {
                timerun = dateGet.Value;
            }
            else
            {
                timerun = timerun.AddMinutes(-12);
            }
            var startTime = timerun;
            var endTime = DateTime.Now.EndDateTime();
            Task.WaitAll();
            if (dateGet == null)
            {
                dateGet = DateTime.Now;
            }
            IEnumerable<ReportQuerryTaltimeIndex> allcdrHaving;
            allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecordingServe2(
                    new core.Request.HandlelFileRecordingRequest()
                    {
                        TimeSelect = dateGet,
                        TimeFrom = startTime,
                        TimeTo = endTime

                    }
             );

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
                    DurationBill = item.DurationBill,
                    Lastapp = item.Lastapp,
                    LastData = item.Lastdata
                };
                reportTalkTime.Duration = item.Duration;
                if (item.Lastapp == "Dial" && item.Disposition == "ANSWERED" && !string.IsNullOrWhiteSpace(item.FileRecording))
                {
                    reportTalkTime.FileRecording = Utils.GetFileRecordingFile(item.FileRecording, item.CallDate);
                    reportTalkTime.DurationReal = reportTalkTime.FileRecording.GetDurationAudio();

                }
                if (usergetByLinecode != null)
                {

                    reportTalkTime.VendorId = 8;
                }

                var resultInsert = await _unitOfWork1.ReportTalkTimeRepository.Add(reportTalkTime);

            }
            Task.WaitAll();
            return await Task.FromResult(0);


        }

        private void ProcessUnitLinked(List<List<ReportTalkTimeIndexModel>> dataHandle)
        {
            foreach (var item1 in dataHandle)
            {
                var checkPhoneStatus = 0;

                if (item1.Count == 2)
                {
                    var itemfist = item1[0];
                    var tiemTwo = item1[1];
                    if (itemfist.Lastapp == "Congestion" || itemfist.Disposition == "FAILED" || tiemTwo.Lastapp == "Congestion" ||
                        tiemTwo.Disposition == "FAILED"
                        )
                    {
                        checkPhoneStatus = 1; //system error
                        continue;
                    }
                    else if (

                        itemfist.Lastapp == "Busy" &&
                        itemfist.Disposition == "ANSWERED")
                    {
                        if (itemfist.Duration < 25)
                        {
                            if (tiemTwo.Duration <= 5)
                            {
                                checkPhoneStatus = 2; // air port
                            }
                            else if (tiemTwo.Duration >= 26)
                            {
                                checkPhoneStatus = 8; // thue bao
                            }

                            else
                            {
                                checkPhoneStatus = 3; // busy 
                            }


                        }
                    }
                    else
                    {
                        checkPhoneStatus = 5;
                    }

                    var livephone = new LivePhone()
                    {
                        NoAgree = itemfist.NoAgree,
                        LineCall = itemfist.LineCode,
                        Phone = itemfist.PhoneLog,
                        StatusCheck = checkPhoneStatus

                    };
                    DataCheck.Add(livephone);



                }
                else
                {
                    var itemmain = item1.FirstOrDefault();

                    if (itemmain.Disposition == "ANSWERED" && itemmain.Lastapp == "Dial")
                    {
                        var livephone = new LivePhone()
                        {
                            NoAgree = itemmain.NoAgree,
                            LineCall = itemmain.LineCode,
                            Phone = itemmain.PhoneLog,
                            StatusCheck = 4

                        };
                        DataCheck.Add(livephone);
                    }
                    else
                    {

                        if (itemmain.PhoneLog != "failed")
                        {



                            if (itemmain.Disposition == "NO ANSWER" && itemmain.Duration > 60)
                            {
                                var livephone = new LivePhone()
                                {
                                    NoAgree = itemmain.NoAgree,
                                    Phone = itemmain.PhoneLog,
                                    LineCall = itemmain.LineCode,
                                    StatusCheck = 6   //  incorect phone

                                };
                                DataCheck.Add(livephone);
                            }
                            else if (itemmain.Disposition == "NO ANSWER")
                            {
                                var livephone = new LivePhone()
                                {
                                    NoAgree = itemmain.NoAgree,
                                    Phone = itemmain.PhoneLog,
                                    LineCall = itemmain.LineCode,
                                    StatusCheck = 5

                                };
                                DataCheck.Add(livephone);
                            }
                            else
                            {
                                var livephone = new LivePhone()
                                {
                                    NoAgree = itemmain.NoAgree,
                                    Phone = itemmain.PhoneLog,
                                    LineCall = itemmain.LineCode,
                                    StatusCheck = 7

                                };
                                DataCheck.Add(livephone);
                            }

                        }



                    }
                }


            }
        }

        public async Task<bool> HandleData()
        {
            var dataHandle = await _unitOfWork1.ReportTalkTimeRepository.GetALl(
                new core.Request.ReportTalkTimeRequest()

                );
            var datas = dataHandle.Data as List<ReportTalkTimeIndexModel>;
            var listGroupByNoAgree = datas.GroupBy(u => u.NoAgree)
            .Select(grp => grp.ToList())
            .ToList();

            foreach (var item in listGroupByNoAgree)
            {
                var isLive = false;
                var dataLiked = item.GroupBy(x => x.Linkedid)
                                    .Select(grp => grp.ToList()).ToList();
                ProcessUnitLinked(dataLiked);


            }
            return true;
        }
    }
}
