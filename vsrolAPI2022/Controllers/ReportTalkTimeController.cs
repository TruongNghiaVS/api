using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.Request;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ReportTalkTimeController : BaseController
    {


        private IReportTalkTimeGroupByDayBussiness _reportTalkTimeGroupByDayBussiness;


        public ReportTalkTimeController(
            IUserBusiness userBusiness,

            IReportTalkTimeGroupByDayBussiness reportTalkTimeGroupByDayBussiness) : base(userBusiness)
        {

            _reportTalkTimeGroupByDayBussiness = reportTalkTimeGroupByDayBussiness;
        }


        [HttpPost("~/api/reportTalkTime/getAll")]
        public async Task<IResult> getAll(GetAllRecordGroupByLineCodeRequest _input)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.RoleId == "2")
            {

            }
            else
            {
                _input.LineCode = currentUser.LineCode;
            }
            var resultSearch = await _reportTalkTimeGroupByDayBussiness.GetAll(_input);
            return Results.Ok(resultSearch);
        }





    }
}