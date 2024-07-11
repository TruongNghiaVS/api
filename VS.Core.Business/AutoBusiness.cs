using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http.Headers;
using VS.core.Request;
using VS.Core.Business.GlobalClass;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{


    public class PhoneLive
    {
        public string Phone { get; set; }


        public int Status { get; set; }
    }


    public class ChanelStatusApi
    {

        public string Response { get; set; }
        public string Actionid { get; set; }
        public string Message { get; set; }

        public List<string> Output { get; set; }
    }
    public class AutoBusiness : BaseBusiness, IAutoBussiness
    {

        public static List<CampagnProfile> ListCall { get; set; }
        private static List<string> ChanelCall { get; set; }
        private DataCallContainer DataCall { get; set; }

        private List<Object> listData = new List<Object>();
        private ICallLogBussiness CallLogBussiness;
        public AutoBusiness(IUnitOfWork unitOfWork, ICallLogBussiness callLogBussiness) : base(unitOfWork)
        {
            ListCall = new List<CampagnProfile>();
            ChanelCall = new List<string>();
            ChanelCall.Add("3000");
            ChanelCall.Add("3200");
            ChanelCall.Add("3201");
            DataCall = DataCallContainer.GlobalContainer();
            CallLogBussiness = callLogBussiness;
        }

        public Task<GetAllProfileByCampangReponse> GetAllCampagn(
            GetAllProfileByCampang request
        )
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }

        public Task<CampagnProfile> GetProfileCall(

      )
        {
            return _unitOfWork.CampagnProfileRe.GetProfileCall();
        }
        public async Task<bool> UpdateCampagnAuto(
            string id, bool result
         )
        {
            await _unitOfWork.CampagnProfileRe.UpdateCampagnAuto(id, result);
            return true;
        }

        public async Task<List<ReportQuerryCallResult>> GetInfomationCall(string linecode,
            string phoneNumber)
        {
            var data = await _unitOfWork.CampagnProfileRe.GetInfomationCall(linecode, phoneNumber);
            return data;
        }
        private async Task<bool> MakeCall(string phoneNumber, string chanel)
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phoneNumber = phoneNumber,
                userid = 1,
                lineCode = chanel
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.10:3002";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                var reponse = await client.PostAsync("api/client/makeCall", data);
                var result = await reponse.Content.ReadAsStringAsync();
            }
            return true;
        }
        private async Task ACD(string chanel)
        {
            if (!DataCall.CheckValidCall())
            {
                return;
            }
            var itemCall = DataCall.Data.First();
            var itemInsert = new LogCall
            {
                CreateAt = DateTime.Now,
                CreatedBy = "-1",
                Deleted = false,
                Phone = itemCall.Phone,
                NoAgree = itemCall.NoAgree,
                TimeBuisiness = DateTime.Now,
                VendorId = 8,
                ProfileId = 1,
                UserId = "-1",
                LineCode = chanel
            };
            await CallLogBussiness.Add(itemInsert);
            await MakeCall(itemCall.Phone, chanel);
            DataCall.Data.Remove(itemCall);
        }
        public async Task RunTask()
        {
            if (listData.Count < 1)
            {
                await LoadData();
            }
            await CenterCall();


        }
        public async Task<bool> Run()
        {


            await CenterCall();
            return true;
        }
        private string ReadvalueStringExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Value != null)
                {
                    return cellRange.Value.ToString();
                }

            }
            return "";

        }

        public async Task<bool> LoadData()
        {
            var filestrem = new FileStream("C:\\Users\\Admin\\Desktop\\sourceData\\09.07.24.xlsx", FileMode.Open);
            await using (MemoryStream ms = new MemoryStream())
            {
                await filestrem.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var noAgree = ReadvalueStringExcel(workSheet, i, 1);
                        var phoneNumber = ReadvalueStringExcel(workSheet, i, 26);
                        DataCall.AddUser(phoneNumber, noAgree);
                    }
                }
            }
            return true;

        }

        public async Task<bool> LoadDataCall()
        {

            var requestSearch = new GetAllProfileByCampang()
            {

            };
            var dataCal = _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(requestSearch);
            var filestrem = new FileStream("C:\\Users\\Admin\\Desktop\\sourceData\\09.07.24.xlsx", FileMode.Open);
            await using (MemoryStream ms = new MemoryStream())
            {
                await filestrem.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var noAgree = ReadvalueStringExcel(workSheet, i, 1);
                        var phoneNumber = ReadvalueStringExcel(workSheet, i, 26);
                        DataCall.AddUser(phoneNumber, noAgree);
                    }
                }
            }
            return true;

        }
        private async Task<bool> CenterCall()
        {

            if (!DataCall.CheckValidCall())
            {
                return true;
            }
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.10:3002";
            var listActive = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                var reponse = await client.PostAsync("api/chanelGetStatus", data);
                var result = await reponse.Content.ReadAsStringAsync();
                var allChanel = new List<string>();
                var resultPart = JsonConvert
                                .DeserializeObject<ChanelStatusApi>(result);
                foreach (var item in resultPart.Output)
                {
                    var textArray = item.Split(' ');
                    if (textArray.Length < 1 || textArray[0].Length < 8)
                    {
                        continue;
                    }
                    allChanel.Add(textArray[0]);
                }
                listActive = ChanelCall.Where(p => allChanel.All(p2 => !p2.Contains(p))).ToList();
                if (listActive == null)
                {
                    return true;
                }
            }

            foreach (var item2 in listActive)
            {
                await ACD(item2);
            }
            return true;

        }
        public async Task<bool> GetData()
        {
            await GetProfileCall();
            await MakeCall("0383338840", "3000");
            return true;
        }

        private async Task HandleTwoCase(IGrouping<string, ReportQuerryCallResult> listHandle,
            List<PhoneLive> DataLists)
        {
            var itemlist = listHandle.ToList();
            var item1 = itemlist[0];
            var item2 = itemlist[1];
            var itemRecord = new ReportTalkTime()
            {
                DurationBill = item1.DurationBill,
                DurationReal = item1.DurationBill,
                Lastapp = item1.Lastapp,
                Disposition = item1.Disposition,
                FileRecording = item1.FileRecording,
                CallDate = item1.CallDate,
                CreateAt = DateTime.Now,
                Duration = item1.Duration,
                EventTime = item1.EventTime,
                NoAgree = item1.NoAgree,
                Linkedid = item1.Linkedid,
                PhoneLog = item1.PhoneLog,
                VendorId = -1,
                CompanyId = -1,
                LastData = item1.Lastdata,
                LineCode = item1.LineCode,
                CreatedBy = "-1",
                CampangnId = -1,
                UpdatedBy = "-1",
                Sourcecall = -1,
                Deleted = false
            };
            int statusCall;
            if (item1.Lastapp == "Dial" && item1.Disposition == "BUSY")
            {
                if (item1.DurationBill > 30)
                    statusCall = 2;

                else if (item1.DurationBill > 18 && item1.DurationBill <= 25)
                    statusCall = 4;

                else
                    statusCall = 0;

            }
            else if (item1.Lastapp == "Dial" && item1.Disposition == "NO ANSWER")
            {
                if (item2.Lastapp == "Congestion")
                    statusCall = 3;
                else
                    statusCall = 5;

            }
            else
            {
                statusCall = 6;
            }
            DataLists.Add(new PhoneLive()
            {
                Phone = item1.PhoneLog,
                Status = statusCall
            });
            itemRecord.StatusCall = statusCall;
            await _unitOfWork.ReportTalkTimeRepository.Add(itemRecord);
        }


        private async Task HandleOneCase(IGrouping<string, ReportQuerryCallResult> listHandle,
            List<PhoneLive> DataLists)
        {
            var itemMain = listHandle.FirstOrDefault();
            if (itemMain == null)
            {
                return;
            }
            var itemRecord = new ReportTalkTime()
            {
                DurationBill = itemMain.DurationBill,
                DurationReal = itemMain.DurationBill,
                Lastapp = itemMain.Lastapp,
                Disposition = itemMain.Disposition,
                FileRecording = itemMain.FileRecording,
                CallDate = itemMain.CallDate,
                CreateAt = DateTime.Now,
                Duration = itemMain.Duration,
                EventTime = itemMain.EventTime,
                NoAgree = itemMain.NoAgree,
                Linkedid = itemMain.Linkedid,
                PhoneLog = itemMain.PhoneLog,
                VendorId = -1,
                CompanyId = -1,
                LastData = itemMain.Lastdata,
                LineCode = itemMain.LineCode,
                CreatedBy = "-1",
                CampangnId = -1,
                UpdatedBy = "-1",
                Sourcecall = -1,
                Deleted = false
            };
            var statusCall = -1;


            if (itemMain.Lastapp == "Dial" && itemMain.Disposition == "ANSWERED")
            {
                if (itemMain.DurationBill > 105 && itemMain.DurationBill <= 110 || itemMain.DurationBill == 110 || itemMain.DurationBill == 120)
                {

                    statusCall = 8;
                }
                else
                {

                    statusCall = 1;
                }
            }
            else if (itemMain.Lastapp == "Dial" && itemMain.Disposition == "BUSY")
            {
                statusCall = 0;
                if (itemMain.DurationBill > 30)
                {
                    statusCall = 2;
                }
                else if (itemMain.DurationBill > 18 && itemMain.DurationBill <= 25)
                {

                    statusCall = 4;
                }
                else
                {
                    statusCall = 0;
                }

            }
            else if (itemMain.Lastapp == "Dial" && itemMain.Disposition == "NO ANSWER")
            {
                statusCall = 5;

            }
            else if (itemMain.Lastapp == "Dial" && itemMain.Disposition == "FAILED")
            {
                statusCall = 9;

            }
            else if (itemMain.Lastapp == "Congestion")
            {
                statusCall = 3;

            }

            DataLists.Add(new PhoneLive()
            {
                Phone = itemMain.PhoneLog,
                Status = statusCall

            });
            itemRecord.StatusCall = statusCall;
            await _unitOfWork.ReportTalkTimeRepository.Add(itemRecord);

        }


        private async Task HandleCase(IGrouping<string, ReportQuerryCallResult> listHandle,
            List<PhoneLive> DataLists)
        {

            if (listHandle.Count() < 2)
            {
                await HandleOneCase(listHandle, DataLists);
                return;
            }
            await HandleTwoCase(listHandle, DataLists);

        }

        public async Task<bool> HandleAutoBussiness()
        {
            var infomationResult = await GetInfomationCall("3000", "");
            var groupByLineCode = infomationResult.GroupBy(x => x.LineCode);
            var phoneLive = new List<PhoneLive>();
            foreach (var itemgroup in groupByLineCode)
            {
                var linkEds = itemgroup.GroupBy(x => x.Linkedid).ToList();
                foreach (var itemRow in linkEds)
                {
                    await HandleCase(itemRow, phoneLive);
                }
            }
            return true;
        }

    }
}
