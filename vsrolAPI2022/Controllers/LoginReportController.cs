
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Business.Interface;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LoginReportController : BaseController
    {

        private readonly ILoginReportBussiness _impactBusiness;
        private readonly IHandleReportBussiness _handleReportBussiness;


        public LoginReportController(ILoginReportBussiness campagnBusiness,
            IHandleReportBussiness handleReportBussiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _handleReportBussiness = handleReportBussiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/loginReport/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _impactBusiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);

        }


        [AllowAnonymous]
        [HttpPost("~/api/loginReport/getAll")]
        public async Task<IResult> GetAll(LoginReportSerarchRequest request)
        {
            var user = GetCurrentUser();

            var searchRequest = new LoginReportSerarchRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,

                From = request.From
            };

            if (user.RoleId != "2")
            {
                searchRequest.UserName = user.UserName;
            }
            var resultSearch = await _impactBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/loginReport/add")]
        public async Task<IResult> Add(LoginReportAdd employeeAdd)
        {
            var user = GetCurrentUser();

            var account = new LoginReport()
            {


            };
            var result = await _impactBusiness.AddAsync(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/loginReport/update")]
        public async Task<IResult> Update(LoginReportUpdate request)
        {
            //var user = GetCurrentUser();

            var accoutUpdate = await _impactBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.UpdatedBy = "1";
            var result = await _impactBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }



        [AllowAnonymous]
        [HttpPost("~/api/loginReport/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _impactBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {
                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _impactBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }


        [AllowAnonymous]
        [HttpPost("~/api/loginReport/calculatorTalkingTime")]
        public async Task<IResult> CalculatorTalkingTime(DeleteModelRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            //var accoutDelete = await _impactBusiness.GetByIdAsync(request.Id);
            //if (accoutDelete == null)
            //{
            //    return Results.Ok(new ErrorReponse()
            //    {
            //        StatusCode = StatusCodes.Status200OK,
            //        Success = false,
            //        Message = _message.CommonError_NotFound

            //    });
            //}
            await _handleReportBussiness.CalTalkingTime();

            return Results.Ok(0);
        }

    }
}