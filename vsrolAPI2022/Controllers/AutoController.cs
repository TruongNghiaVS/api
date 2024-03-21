using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.Core.Business;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using Newtonsoft.Json;
using System.Net.Http.Headers;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]

    public class AutoController : BaseController
    {
        private readonly IAutoBussiness _business;
        public AutoController(
            IAutoBussiness autoBussiness,
            IUserBusiness userBusiness
           
        ) : base(userBusiness)
        {
            _business = autoBussiness;
        }
       
        private async Task<IResult> MakeCall(string phoneNumber = "")
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
                return Results.Ok(result);
            }
        }

        [AllowAnonymous]
        [HttpGet("~/api/auto/handlerAutoCall")]
        public async Task<IResult> HandlerAutoCall()
        {
            //lay danh sach
            var camprofileGet = await _business.GetProfileCall();
            await MakeCall("0383338840");
            return Results.Ok(new { success = true });
        }

        [AllowAnonymous]
        [HttpGet("~/api/auto/handleBusinessCall")]
        public async Task<IResult> HandleBusinessCall()
        {
            //lay danh sach
            var camprofileGet = await _business.GetProfileCall();
            await MakeCall("0383338840");
            return Results.Ok(new { success = true });
        }

    }
}
