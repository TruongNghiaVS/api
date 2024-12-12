using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.core.AutoCall;
using VS.Core.Business.Interface;
namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ACDController : BaseController
    {
        private readonly IAutoBussiness _business;
        private readonly IServiceBussiness _serviceBussiness;
        public ACDController(
            IAutoBussiness autoBussiness,
            IUserBusiness userBusiness,
            IServiceBussiness serviceBussiness
       ) : base(userBusiness)
        {
            _business = autoBussiness;
            _serviceBussiness = serviceBussiness;
        }

        [HttpPost("~/api/ACD/run")]
        public async Task<IResult> HandlerAutoCall()
        {
            await _business.MakeCall();
            return Results.Ok(new { success = true });
        }

        [AllowAnonymous]
        [HttpPost("~/api/ACD/LoadData")]
        public async Task<IResult> LoadData()
        {
            await _business.LoadData();
            return Results.Ok(new { success = true });
        }
        [HttpPost("~/api/ACD/GetInfomationCall")]
        public async Task<IResult> GetInfomationCall()
        {
            var user = GetCurrentUser();
            var result = await _business.GetInfomation(user.LineCode);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("~/api/ACD/TurnOfAutoCall")]
        public async Task<IResult> TurnOfAutoCall()
        {
            var result = await _business.TurnOffAutoCall(true);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("~/api/ACD/TurnOnAutocall")]
        public async Task<IResult> TurnOnAutocall()
        {
            var result = await _business.TurnOffAutoCall(false);
            return Results.Ok(result);
        }

        [HttpPost("~/api/ACD/GetDurationTalk")]
        public async Task<IResult> GetDurationAutocall(AutoCallRequest request)
        {
            var noAgree = request.NoAgreeId;
            var user = GetCurrentUser();
            var result = await _business.GetDurationAutocall(noAgree);
            return Results.Ok(result);
        }


        [AllowAnonymous]
        [HttpGet("~/api/ACD/SetChanel")]
        public async Task<IResult> SetChanel(int limitchanel = 25)
        {
            var result = await _business.SetChanel(limitchanel);

            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("~/api/ACD/SetLoadDataSip")]
        public async Task<IResult> SetLoadDataSip(bool loadSip = false)
        {
            var result = await _business.SetLoadDataSip(loadSip);
            return Results.Ok(result);
        }


    }
}

