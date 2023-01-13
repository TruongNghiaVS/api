
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ProcessingController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        private readonly ICallLogBussiness _callLogBussiness;
        public ProcessingController(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICallLogBussiness callLogBussiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _callLogBussiness = callLogBussiness;
        }


        [HttpPost("~/api/call/makeCall")]
        public async Task<IResult> MakeCall(MakeCallProcessRequest inputRequest)
        {
            var userCall = GetCurrentUser();
            if (string.IsNullOrEmpty(inputRequest.PhoneLog) ||
                string.IsNullOrEmpty(inputRequest.NoAgree))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var callLogInsert = new CallLog()
            {
                CreateAt = DateTime.Now,
                CreatedBy = userCall.Id,
                NoAgree = inputRequest.NoAgree,
                PhoneLog = inputRequest.PhoneLog,
                LineCode = userCall.LineCode,
                UpdatedBy = userCall.Id
            };
            var ressult = await _callLogBussiness.AddAsync(callLogInsert);
            var ressultReponse = new
            {
                success = ressult > 0

            };
            return Results.Ok(ressultReponse);

        }






    }
}