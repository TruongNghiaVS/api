using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace VS.core.AutoCall
{

    public class TrackingCallApi
    {

        public string Response { get; set; }
        public string Actionid { get; set; }
        public string Message { get; set; }

        public List<string> Output { get; set; }
    }

    public class TrackingCallAs
    {
        public string Channel { get; set; }
        public string Context { get; set; }
        public string Extension { get; set; }
        public string Prio { get; set; }
        public string State { get; set; }
        public string Application { get; set; }
        public string Data { get; set; }
        public string CallerID { get; set; }
        public string Duration { get; set; }
        public string Accountcode { get; set; }
        public string PeerAccount { get; set; }
    }
    public class AutoBussiness : IAutoBussiness
    {
        private AutocallApi bussiness { get; set; }

        private DataAccess dataAccess;

        private readonly IServiceBussiness serviceBussiness;
        ModelData ModelData2;

        public AutoBussiness(IServiceBussiness _serviceBussiness)
        {
            bussiness = new AutocallApi();

            serviceBussiness = _serviceBussiness;
            dataAccess = new DataAccess();
            ModelData2 = ModelData.Instance;
        }

        public async Task<bool> SetChanel(int limitChange = 25)
        {
            ModelData2.LimitChanel = limitChange;
            return true;
        }

        public async Task<bool> SetLoadDataSip(bool loadSip = false)
        {
            ModelData2.LoadSip = loadSip;
            return true;
        }



        public async Task<List<PhoneLog>> LoadData()
        {
            ModelData2.DataCall.Clear();
            var listNew = await dataAccess.GetAllData(ModelData2.LoadSip);
            ModelData2.DataCall = listNew;
            foreach (var item in listNew)
            {
                ModelData2.DataCallQueue.Enqueue(item);
            }
            return listNew;
        }
        public async Task<bool> MakeCall()
        {
            if (ModelData2 == null)
            {
                ModelData2 = ModelData.Instance;
            }
            if (ModelData2.IsTurnOfAutocall == true)
            {
                return false;
            }
            if (ModelData2.DataCallQueue.Count < 1)
            {
                return false;
            }
            if (ModelData2.CountDataRing > ModelData2.LimitChanel - 3)
            {
                return false;
            }
            int loopRun = ModelData2.LimitChanel - ModelData2.CountDataRing;
            var indexRow = ModelData2.DataCallQueue.Count;
            if (indexRow > loopRun)
            {
                indexRow = loopRun;
            }
            var listRun = new List<PhoneLog>();
            for (int i = 0; i < indexRow; i++)
            {
                var itemIndex = ModelData2.DataCallQueue.Dequeue();
                listRun.Add(itemIndex);
            }

            foreach (var item in listRun)
            {
                ModelData2.DataCalled.Add(item);
                if (string.IsNullOrEmpty(item.Id))
                {
                    continue;
                }
                var idNumber = int.Parse(item.Id);
                var sip = "";
                if (ModelData2.LoadSip)
                {
                    sip = item.LineCode;
                }
                await serviceBussiness.CallNumber(item.MobilePhone, sip, idNumber);
            }
            return true;
        }

        public async Task<bool> TurnOffAutoCall(bool turnof)
        {

            ModelData2.IsTurnOfAutocall = turnof;
            return true;

        }
        public bool IsValidPhoneNumber(string phoneCheck)
        {
            if (phoneCheck.Length < 9 || phoneCheck.Length > 12)
            {
                return false;
            }
            var regex = "^(0|84)(2(0[3-9]|1[0-6|8|9]|2[0-2|5-9]|3[2-9]|4[0-9]|5[1|2|4-9]|6[0-3|9]|7[0-7]|8[0-9]|9[0-4|6|7|9])|3[2-9]|5[5|6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])([0-9]{7})$";
            var phoneNumber = phoneCheck.Trim()
                .Replace("\"", "")
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }
        private bool IsValidChanel(string chanelCheck)
        {
            if (chanelCheck.Length != 4)
            {
                return false;
            }
            var regex = @"\d{4}";
            var phoneNumber = chanelCheck.Trim()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }
        private bool IsValidTiemHour(string This)
        {
            var regex = "^(?:(?:([01]?\\d|2[0-3]):)?([0-5]?\\d):)?([0-5]?\\d)$";
            var phoneNumber = This.Trim()
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "");
            return Regex.Match(phoneNumber, regex).Success;
        }

        public async Task<DurationTalkReponse> GetDurationAutocall(string noAgree)
        {
            var resultReponse = new DurationTalkReponse();
            var itemCall = ModelData2.DataCall.Where(x => x.Id == noAgree).FirstOrDefault();
            if (itemCall == null)
            {
                return resultReponse;
            }
            var phonelog = itemCall.MobilePhone;
            if (ModelData2.DataCall == null || ModelData2.DataCall.Count < 1)
            {
                await LoadData();
            }
            var customerName = ModelData2.DataCall
                                .Where(x => x.MobilePhone == phonelog)
                                .FirstOrDefault();
            resultReponse.PhoneNumber = phonelog;
            resultReponse.FullName = customerName.CustomerName;
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151:3002";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                var reponse = await client.PostAsync("api/chanelGetStatus", data);
                var result = await reponse.Content.ReadAsStringAsync();
                var resultPart = JsonConvert.DeserializeObject<TrackingCallApi>(result);
                var listData = new List<TrackingCallAs>();
                foreach (var item in resultPart.Output)
                {
                    var cdrItem = Regex.Split(item.Replace("(Outgoing Line)", "(OutgoingLine)").Replace(" ", "       "), @"\s\s+");
                    var phoneNumber = "";
                    var chanel = "";
                    var timehour = "";
                    foreach (var item1 in cdrItem)
                    {
                        if (IsValidPhoneNumber(item1))
                        {
                            phoneNumber = item1;
                        }
                        if (IsValidChanel(item1))
                        {
                            chanel = item1;
                        }
                        if (IsValidTiemHour(item1))
                        {
                            timehour = item1;
                        }


                    }
                    if (phonelog == phoneNumber)
                    {
                        resultReponse.Duration = timehour;
                        resultReponse.HavingCall = true;
                        return resultReponse;
                    }
                    var callidtemp = chanel;
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        callidtemp = phoneNumber;
                    }
                    string lineInput = chanel;
                    if (lineInput == "")
                    {
                        continue;
                    }
                    if (phoneNumber == phonelog)
                    {
                        resultReponse.Duration = timehour;
                        resultReponse.HavingCall = true;
                        return resultReponse;
                    }
                }
            }
            return resultReponse;

        }
        public async Task<dynamic> GetInfomation(string lineCode)
        {
            var allResult = ModelData2.DataRing;
            var resultInfomation = allResult
                .Where(x => x.Sip == lineCode)
                .FirstOrDefault();
            if (resultInfomation == null)
            {
                return "";
            }
            var itemCall = ModelData2.DataCalled
                .Where(x => x.MobilePhone == resultInfomation.Phone)
                .FirstOrDefault();
            if (itemCall == null)
            {
                return "";
            }
            var itemInfo = new
            {
                linkHref = "/follow-up/" + itemCall.Id + "?autocalFor=" + itemCall.Id,
                MobilePhone = itemCall.MobilePhone,
                Name = itemCall.CustomerName
            };
            return itemInfo;
        }

    }
}
