using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class TrackingController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;

        private readonly ISkipInfoBussiness _skipInfoBussiness;
        public TrackingController(ICampagnBussiness campagnBusiness,
            ISkipInfoBussiness skipInfoBussiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _skipInfoBussiness = skipInfoBussiness;
        }


        [HttpPost("~/api/tracking/requestCheck")]
        public async Task<IResult> requestCheck()
        {

            var resultcheck = new
            {
                sucecss = false
            };


            return Results.Ok(resultcheck);


        }

        public static bool IsValidPhoneNumber(string phoneCheck)
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


        [HttpPost("~/api/tracking/Test")]
        public async Task<ActionResult> Test()
        {
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
                TrackingCallController.OutPutData.ForEach(c => c.IsCalling = false);
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

                    var callidtemp = chanel;
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        callidtemp = phoneNumber;
                    }
                    string lineInput = chanel;
                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        var tiemCall = MakeCallController.GlobalLogCall.Where(x => x.Phone == callidtemp)
                            .FirstOrDefault();
                        if (tiemCall != null)
                        {
                            lineInput = tiemCall.LineCode;
                        }
                    }

                    if (lineInput == "")
                    {
                        continue;
                    }

                    var itemGroupUpdate = TrackingCallController.OutPutData.Where(x => x.LineCode == lineInput).FirstOrDefault();
                    if (itemGroupUpdate == null)
                    {
                    }
                    else
                    {
                        itemGroupUpdate.DurationRealTime = timehour;
                        itemGroupUpdate.IsCalling = true;
                    }
                }


            }
            return Ok();
        }



    }
}