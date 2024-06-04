using IronXL;
using Newtonsoft.Json;
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
        public AutoBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ListCall = new List<CampagnProfile>();
            ChanelCall = new List<string>();
            ChanelCall.Add("3000");
            //ChanelCall.Add("9999");
            DataCall = DataCallContainer.GlobalContainer();

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
            if (listData.Count < 1)
            {
                return;
            }
            var itemCall = listData.First() as dynamic;
            await MakeCall(itemCall.phone1, chanel);
            listData.Remove(itemCall);
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
            if (listData.Count < 1)
            {
                await LoadData();
            }
            await CenterCall();

            return true;
        }

        public async Task<bool> LoadData()
        {
            WorkBook workBook = WorkBook.Load("C:\\Users\\Admin\\Desktop\\fileMirae.xlsx");
            WorkSheet workSheet = workBook.WorkSheets.First();
            for (int i = 0; i < workSheet.RowCount; i++)
            {
                if (i < 2)
                {
                    continue;
                }
                var inputNoAgree = workSheet["A" + i].StringValue;
                var inputNoPhone = workSheet["Z" + i].StringValue;
                listData.Add(new
                {
                    noAgree = inputNoAgree,
                    phone1 = inputNoPhone
                });
            }
            return true;
        }

        private async Task<bool> CenterCall()
        {

            if (!listData.Any())
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
            if (item1.Lastapp == "Dial" && item1.Disposition == "BUSY")
            {
                if (item1.DurationBill > 40)  // case goi khong bat may ( de troi)
                {
                    DataLists.Add(new PhoneLive()
                    {
                        Phone = item1.PhoneLog,
                        Status = 2  //goi khong bat may

                    });
                    return;
                }
                else if (item2.DurationBill > 20 && item2.DurationBill <= 25)
                {
                    DataLists.Add(new PhoneLive()
                    {
                        Phone = item1.PhoneLog,
                        Status = 4  //goi thuê bao

                    });
                    return;
                }
                //case may bay

                // case busy . thue bao
                DataLists.Add(new PhoneLive()
                {
                    Phone = item1.PhoneLog,
                    Status = 0  //busy  

                });

                return;


            }
            else if (item1.Lastapp == "Dial" && item1.Disposition == "NO ANSWER")
            {

                if (item2.Lastapp == "Congestion")
                {
                    DataLists.Add(new PhoneLive()  // số điện thoại không đúng
                    {
                        Phone = item1.PhoneLog,
                        Status = 3

                    });
                }

                else
                {
                    DataLists.Add(new PhoneLive()  // số điện thoại không đúng cas2
                    {
                        Phone = item1.PhoneLog,
                        Status = 5
                    });
                }

            }

            else
            {
                DataLists.Add(new PhoneLive()  // số điện thoại không đúng
                {
                    Phone = item1.PhoneLog,
                    Status = 6 // no underfile
                });
            }
        }

        private async Task HandleCase(IGrouping<string, ReportQuerryCallResult> listHandle,
            List<PhoneLive> DataLists)
        {
            if (listHandle.Count() < 2)
            {
                //await HandleOneCase(listHandle, DataLists);
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
