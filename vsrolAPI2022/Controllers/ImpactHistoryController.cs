
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.Campagn;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ImpactHistoryController : BaseController
    {

        private readonly IImpactHistoryBussiness _impactBusiness;

        private readonly ICampagnBussiness _campagnBussiness;

        public ImpactHistoryController(IImpactHistoryBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICampagnBussiness campagnBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _campagnBussiness = campagnBussiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/impacthistory/getById")]
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
        [HttpPost("~/api/impacthistory/getAll")]
        public async Task<IResult> GetAll(ImpactHistorySearchInput request)
        {
            //var user = GetCurrentUser();
            var searchRequest = new ImpactHistorySerarchRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _impactBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/impacthistory/add")]
        public async Task<IResult> Add(ImpactHistoryAdd employeeAdd)
        {
            var user = GetCurrentUser();

            var lineCode = "";
            if (user != null)
            {
                lineCode = user.LineCode;
            }
            var campangnProfile = await _campagnBussiness.GetProfile(employeeAdd.ProfileId.Value.ToString());

            var itemInsert = new ImpactHistory()
            {
                NoteIm = employeeAdd.NoteIm,
                MoneyPromise = employeeAdd.MoneyPromise,
                Promiseday = employeeAdd.Promiseday,
                ProfileId = employeeAdd.ProfileId,
                Relationship = employeeAdd.Relationship,
                Priority = 1,
                CreatedBy = user.Id,
                StatusFollow = employeeAdd.StatusFollow,
                StatusIm = employeeAdd.StatusIm,
                ShortDescription = employeeAdd.NoteIm,
                DaysuggestTime = employeeAdd.DaysuggestTime,
                UpdatedBy = user.Id,
                AssigeeId = campangnProfile.Assignee,
                LineCode = lineCode

            };

            if (campangnProfile.Status == 16)
            {

            }
            else
            {
                campangnProfile.Reasonstatus = int.Parse(employeeAdd.StatusIm);
                campangnProfile.Status = int.Parse(employeeAdd.StatusIm);
                await _campagnBussiness.UpdateProfile(campangnProfile);
            }
            var result = await _impactBusiness.AddAsync(itemInsert);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/impacthistory/update")]
        public async Task<IResult> Update(ImpactHistoryUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }
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
        [HttpPost("~/api/impacthistory/delete")]
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

    }
}