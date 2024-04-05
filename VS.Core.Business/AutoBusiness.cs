using DocumentFormat.OpenXml.VariantTypes;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.Field;
using System.Net.Http.Headers;
using VS.core.Request;
using VS.Core.Business.GlobalClass;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class AutoBusiness : BaseBusiness, IAutoBussiness
    {
         
        public static List<CampagnProfile> ListCall { get; set; }
        public AutoBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            ListCall = new List<CampagnProfile>();
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

        public async Task<bool>  UpdateCampagnAuto(
            string id, bool result
         )
        {
            await _unitOfWork.CampagnProfileRe.UpdateCampagnAuto(id,result);
            return true;
        }

        public async Task<List<ReportQuerryCallResult>> GetInfomationCall( string linecode,
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
        private async Task<bool> MakeCall(string phoneNumber = "")
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phoneNumber = phoneNumber,
                userid = 1,
                lineCode = "3000"
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

        public async Task<bool> Run()
        {
            var camprofileGet = await GetProfileCall();
            ConatinnerCall.Contanner.DataCall.Add(camprofileGet);
            await MakeCall("0383338840");
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
          
            if(ConatinnerCall.Contanner.DataCall.Count < 1)
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
