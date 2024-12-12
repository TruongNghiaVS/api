using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace VS.core.AutoCall
{
    public class WashDataItem
    {

        public string NoAgree { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
    }

    public class CreateTokenReponse
    {
        public string Status { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
    public class WashDataDBItem
    {
        public DateTime Calldate { get; set; }
        public string Src { get; set; }
        public string Dcontext { get; set; }
        public string Disposition { get; set; }
        public int Duration { get; set; }
        public int Billsec { get; set; }
    }

    public class StatusReponse
    {

        public string Status { get; set; }



        public List<StatusItem> Calls { get; set; }
    }

    public class MakeCallReponse
    {

        public string Status { get; set; }
        public MakeCallReponseItem Data { get; set; }

        public string Message { get; set; }
    }

    public class MakeCallReponseItem
    {
        public string Phone_number { get; set; }
        public string Trunk { get; set; }
        public string Extension { get; set; }
        public string Message { get; set; }
    }

    public class StatusItem
    {
        public string Sip { get; set; }
        public string Phone { get; set; }

        public string Status { get; set; }
    }
    public class AutocallApi
    {
        private string Token { get; set; }
        private DataAccess dataAccess;
        private DateTime? DateExprise { get; set; }
        public AutocallApi()
        {
            Token = "";
            DateExprise = null;
            dataAccess = new DataAccess();
        }
        public CreateTokenReponse CreateToken()
        {
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                id = "vietstaru",
                password = "vietstarP@ssw0rd",

            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpYXQiOjE3Mjg2MzgxOTYsImV4cCI6MTcyODY0MTc5NiwiaXNzIjoieW91cl9pc3N1ZXIiLCJkYXRhIjp7InVzZXJJZCI6InZpZXRzdGFydSIsInVzZXJOYW1lIjoidmlldHN0YXJ1In19.6bhpQIgKGY19IsXZKuCmP0kD-SReEcZDWmMwtLvNGTs";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = client.PostAsync("autocall/create_token", data).GetAwaiter().GetResult();

                if (reponse.IsSuccessStatusCode)
                {
                    var responseContent = reponse.Content;
                    var ressult = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                    return JsonConvert.DeserializeObject<CreateTokenReponse>(ressult);

                }

            }
            return new CreateTokenReponse() { Token = "" };

        }
        public async Task<bool> MakeCall(string phoneNumber, string sip, int noId)
        {
            //sip = "3001";
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phone_number = phoneNumber
            }));

            if (!string.IsNullOrEmpty(sip))
            {
                data = new StringContent(JsonConvert.SerializeObject(new
                {
                    phone_number = phoneNumber,
                    sip = sip
                }));
            }
            if (string.IsNullOrEmpty(Token) || DateTime.Now > DateExprise.Value.AddMinutes(-10))
            {
                var resultToken = CreateToken();
                Token = resultToken.Token;
                DateExprise = resultToken.ExpiresAt;
            }
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = Token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = client.PostAsync("autocall/make_call", data).GetAwaiter().GetResult();
                if (reponse.IsSuccessStatusCode)
                {
                    var responseContent = await reponse.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<MakeCallReponse>(responseContent);
                    //Write to a file
                    //using (StreamWriter writer = new StreamWriter("C:\\Users\\Administrator\\Desktop\\autocall\\logMakeCall.txt", true))
                    //{
                    //    writer.WriteLine(result.Data.Extension + ";" + result.Data.Phone_number);
                    //}
                    var dataResult = result.Data;
                    await dataAccess.AddLogCall(dataResult.Phone_number,
                        noId, dataResult.Extension);
                    return true;


                };

            }
            return true;

        }
        public async Task<StatusReponse> GetStatusList()
        {
            var result = new StatusReponse();
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
            }));
            if (string.IsNullOrEmpty(Token) || DateTime.Now > DateExprise.Value.AddMinutes(-10))
            {
                var resultToken = CreateToken();
                Token = resultToken.Token;
                DateExprise = resultToken.ExpiresAt;
            }
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var linkUrl = "http://192.168.1.151";
            var accessToken = Token;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var reponse = await client.GetAsync("autocall/get_pjsip_status");
                var resultApi = await reponse.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<StatusReponse>(resultApi);
                return result;
            }
            return new StatusReponse();
        }
    }
}
