
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

        private readonly ICallLogBussiness _logBussiness;
        public ImpactHistoryController(IImpactHistoryBussiness campagnBusiness,
            IUserBusiness userBusiness,
            ICampagnBussiness campagnBussiness,
            ICallLogBussiness logBussiness) : base(userBusiness)
        {
            _impactBusiness = campagnBusiness;
            _campagnBussiness = campagnBussiness;
            _logBussiness = logBussiness;
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
        
        [HttpPost("~/api/impacthistory/exportFinal")]
        public async Task<IResult> GetFinal(ImpactHistorySearchInput request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }
            vendorId = user.VendorId;
            var searchRequest = new ImpactHistorySerarchRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = vendorId.HasValue ? vendorId.ToString() : null,
                From = request.From
            };
            var resultSearch = await _impactBusiness.GetFinal(searchRequest);
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
            var checkHasCall = await _logBussiness.CheckBeforeCall(campangnProfile.NoAgreement,
                int.Parse(user.Id));
            if (checkHasCall ==false)
            {
                return Results.Ok(new
                {
                    isSave = false,
                    message="Bạn chưa thực hiện cuộc gọi, nên chưa thể lưu lịch sử tác động được"
                });
            }

            var itemInsert = new ImpactHistory()
            {
                NoteIm = employeeAdd.NoteIm,
                MoneyPromise = employeeAdd.MoneyPromise,
                Promiseday = employeeAdd.Promiseday,
                ProfileId = employeeAdd.ProfileId,
                Relationship = employeeAdd.Relationship,
                ColorCode = employeeAdd.ColorCode,
                Priority = 1,
                PlaceCode = employeeAdd.PlaceCode,
                CreatedBy = user.Id,
                StatusFollow = employeeAdd.StatusFollow,
                StatusIm = employeeAdd.StatusIm,
                NoteCode = employeeAdd.NoteCode,
                ShortDescription = employeeAdd.NoteIm,
                DaysuggestTime = employeeAdd.DaysuggestTime,
                UpdatedBy = user.Id,
                AssigeeId = campangnProfile.Assignee,
                LineCode = lineCode,
                WayContact = employeeAdd.WayContact

            };

            if (campangnProfile.Status == 16)
            {

            }
            else
            {
                campangnProfile.Reasonstatus = int.Parse(employeeAdd.StatusIm);
                campangnProfile.Status = int.Parse(employeeAdd.StatusIm);
                campangnProfile.ColorCode = employeeAdd.ColorCode;
                await _campagnBussiness.UpdateProfile(campangnProfile);
            }
            var result = await _impactBusiness.Add(itemInsert);
            var reponse = new
            {
                isSave = true,
                message = "Lưu thành công"
            };
            return Results.Ok(reponse);
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