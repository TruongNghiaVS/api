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
            if (currentUser.RoleId == "2" || currentUser.RoleId == "4")
            {


            }
            else
            {
                _input.LineCode = currentUser.LineCode;
            }

            int? VendorId = null;
            if (currentUser.RoleId == "4")
            {
                VendorId = int.Parse(currentUser.Id);
            }
            _input.VendorId = VendorId;
            _input.UserId = currentUser.Id;

            var resultSearch = await _reportTalkTimeGroupByDayBussiness.GetAll(_input);
            return Results.Ok(resultSearch);
        }




        [HttpPost("~/api/reportTalkTime/exportData")]
        public async Task<IResult> ExportData(GetAllRecordGroupByLineCodeExportRequest _input)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.RoleId == "2" || currentUser.RoleId == "4")
            {

            }
            else
            {
                _input.LineCode = currentUser.LineCode;
            }

            int? VendorId = null;
            if (currentUser.RoleId == "4")
            {
                VendorId = int.Parse(currentUser.Id);
            }
            _input.VendorId = VendorId;
            _input.UserId = currentUser.Id;
            var resultSearch = await _reportTalkTimeGroupByDayBussiness.Export(_input);


            return Results.Ok(resultSearch);
        }





    }
}