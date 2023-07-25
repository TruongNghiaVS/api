
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ViewRecordingController : BaseController
    {


        private readonly IViewRecordingBussiness _viewRecordingBussiness;


        public ViewRecordingController(
            IUserBusiness userBusiness,
            IViewRecordingBussiness viewRecordingBussiness) : base(userBusiness)
        {


            _viewRecordingBussiness = viewRecordingBussiness;
        }




        [HttpPost("~/api/viewRecording/getAll")]
        public async Task<IResult> GetAll(ViewRecordingInput request)
        {
            var user = GetCurrentUser();
            var searchRequest = new ViewRecordingRequest()
            {
                Limit = 10,
                Page = request.Page,
                From = request.From,
                LineCode = request.LineCode,
                To = request.To,
                Token = request.Token,
                UserId = user.Id


            };
            var resultSearch = await _viewRecordingBussiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/viewRecording/add")]
        public async Task<IResult> Add(ViewRecordingAdd employeeAdd)
        {
            var user = GetCurrentUser();
            var lineCode = "";
            if (user != null)
            {
                lineCode = user.LineCode;
            }
            var itemInsert = new ViewRecording()
            {
                UserId = user.Id,
                ProfileId = employeeAdd.ProfileId
            };
            var result = await _viewRecordingBussiness.Add(itemInsert);
            return Results.Ok(result);
        }


    }
}