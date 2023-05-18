using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VS.core.Request;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class MakeCallController : BaseController
    {

        private readonly IReportBussiness _impactBusiness;

        private readonly ICallLogBussiness callLogBussiness;
        private IHandleReportBussiness _handleReportBussiness;
        public MakeCallController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICallLogBussiness _callLogBussiness,
            IHandleReportBussiness handleReportBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            callLogBussiness = _callLogBussiness;
            _handleReportBussiness = handleReportBussiness;
        }
        [HttpPost("~/api/MakeCall/TriggerCall")]
        public async Task<IResult> TriggerCall(MakeCallRequest _input)
        {
            var _userCurrent = GetCurrentUser();

            var linecode = _userCurrent.LineCode;
            if (string.IsNullOrEmpty(linecode))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");

            }
            if (linecode.Length < 4)
            {
                return Results.BadRequest("không gọi được");
            }

            if (string.IsNullOrEmpty(_input.PhoneNumber))
            {
                return Results.BadRequest("Dữ liệu không hợp lệ");

            }
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                phoneNumber = _input.PhoneNumber,
                lineCode = linecode
            }));
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var linkUrl = "http://192.168.1.12:3002";

            if (linecode.StartsWith('1'))
            {
                linkUrl = "http://192.168.1.10:3002";
            }
            if (linecode.StartsWith('3'))
            {
                linkUrl = "http://192.168.1.9:3002";
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(linkUrl);




                var reponse = await client.PostAsync("api/client/makeCall", data);
                var result = await reponse.Content.ReadAsStringAsync();
                await callLogBussiness.Add(new VS.Core.dataEntry.User.LogCall()
                {
                    Phone = _input.PhoneNumber,
                    CreateAt = DateTime.Now,
                    LineCode = linecode,
                    ProfileId = _input.ProfileId,
                    NoAgree = _input.NoAgree,
                    TimeBuisiness = DateTime.Now,
                    VendorId = _userCurrent.VendorId,
                    CreatedBy = _userCurrent.Id,
                    UserId = _userCurrent.Id

                });

                return Results.Ok(result);
            }
        }
    }
}