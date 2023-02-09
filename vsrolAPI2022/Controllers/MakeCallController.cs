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

        private IHandleReportBussiness _handleReportBussiness;
        public MakeCallController(IReportBussiness campagnBusiness,
            IUserBusiness userBusiness,
            IHandleReportBussiness handleReportBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
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
            if (linecode == "122")
            {
                linecode = "9000";

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
            data.Headers.ContentType = new MediaTypeHeaderValue("application/json"); // <--

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.168.1.12:3002");
                var reponse = await client.PostAsync("api/client/makeCall", data);
                var result = await reponse.Content.ReadAsStringAsync();
                return Results.Ok(result);
            }
        }
    }
}