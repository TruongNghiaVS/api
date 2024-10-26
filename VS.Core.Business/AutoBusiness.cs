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



    public class ChanelStatusApi
    {

        public string Response { get; set; }
        public string Actionid { get; set; }
        public string Message { get; set; }

        public List<string> Output { get; set; }
    }
    public class AutoBusiness : BaseBusiness, IAuBussiness
    {

        public static List<CampagnProfile> ListCall { get; set; }


        private static List<string> ChanelCall { get; set; }
        private DataCallContainer DataCall { get; set; }
        public AutoBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ListCall = new List<CampagnProfile>();
            ChanelCall = new List<string>();
            ChanelCall.Add("8888");
            ChanelCall.Add("9999");
            DataCall = DataCallContainer.GlobalContainer();

        }


        public Task<int> Add(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(CampagnProfile entity)
        {
            throw new NotImplementedException();
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
            string phoneNumber

     )
        {
            var data = await _unitOfWork.CampagnProfileRe.GetInfomationCall(linecode, phoneNumber);
            return data;
        }


        public Task<CampagnProfile> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
        private async Task<bool> MakeCall(string phoneNumber = "", string chanel = "8888")
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phoneNumber = phoneNumber,
                userid = 1,
                lineCode = chanel
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151:3002";
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
            var itemCall = await DataCall.GetDataCall();
            await MakeCall(itemCall.Phone, chanel);
            DataCall.RemoveData(itemCall.Phone, itemCall.NoAgree);

        }
        public async Task<bool> Run()
        {   //load data


            await CenterCall();

            return true;



        }


        private async Task<bool> CenterCall()
        {

            if (!DataCall.CheckValidCall())
            {
                return false;
            }
            var data = new StringContent(JsonConvert.SerializeObject(new
            {

            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151:3002";
            var listActive = new List<string>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                var reponse = await client.PostAsync("api/chanelGetStatus", data);
                var result = await reponse.Content.ReadAsStringAsync();
                var allChanel = new List<string>();
                var resultPart = JsonConvert.DeserializeObject<ChanelStatusApi>(result);
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



        private bool LoadDataCall()
        {
            return true;
        }
        public async Task<bool> GetData()
        {
            var camprofileGet = await GetProfileCall();
            await MakeCall("0383338840");
            return true;

        }

        public Task<int> UpdateAsyn(CampagnProfile entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HandleAutoBussiness()
        {

            if (ConatinnerCall.Contanner.DataCall.Count < 1)
            {
                ConatinnerCall.Contanner.DataCall.Add(
                new CampagnProfile()
                {
                    Id = "860386"
                });
            }
            var callList = ConatinnerCall.Contanner.DataCall;

            foreach (var item in callList.ToList())
            {
                var phoneNumber = "0383338840";
                var infomationResult = await GetInfomationCall("3000", phoneNumber);

                var islive = false;
                var Lastdata = "";
                foreach (var item1 in infomationResult)
                {
                    Lastdata = item1.Lastdata;
                    if (item1.Disposition == "ANSWERED")
                    {

                        islive = true;

                    }

                }

                callList.Remove(item);
                await UpdateCampagnAuto(item.Id.ToString(), islive);

            }
            return true;
        }

    }
}
