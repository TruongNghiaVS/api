using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CampagnProfileController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        public CampagnProfileController(
            ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _campagnBusiness.GetProfile(inputRequest.Id);
            return Results.Ok(result);

        }


        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/getAll")]
        public async Task<IResult> getAll(CampagnProfileSearchInput request)
        {
            //var user = GetCurrentUser();
            var searchRequest = new GetAllProfileByCampang()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                Id = request.Id,
                From = request.From
            };
            var resultSearch = await _campagnBusiness.GetALlProfileByCampaign(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/add")]
        public async Task<IResult> Add(CampagnProfileAdd employeeAdd)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(employeeAdd.NoAgreement))
            {
                return Results.BadRequest("Không có thông tin mã hợp đồng");
            }
            //var resultcheck = await _campagnBusiness.CheckDuplicate(employeeAdd.Code);

            //if (resultcheck == true)
            //{
            //    return Results.BadRequest("Bị trùng thông tin tên đăng nhập hoặc số điện thoại");
            //}
            var account = new Profile()
            {

            };
            var result = await _campagnBusiness.AddProfile(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/update")]
        public async Task<IResult> Update(CampagnProfileUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }
            var accoutUpdate = await _campagnBusiness.GetProfile(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }

            //accoutUpdate.DisplayName = request.DisplayName;

            //accoutUpdate.UpdatedBy = "1";
            //accoutUpdate.ShortDes = request.ShortDes;
            //accoutUpdate.SumCount = request.SumCount;
            //accoutUpdate.ProcessingCount = request.ProcessingCount;
            //accoutUpdate.ClosedCount = request.ClosedCount;
            //accoutUpdate.Status = request.Status;
            //accoutUpdate.BeginTime = request.BeginTime;
            //accoutUpdate.EndTime = request.EndTime;
            //accoutUpdate.Priority = request.Priority;
            //accoutUpdate.UpdatedBy = "1";
            var result = await _campagnBusiness.UpdateProfile(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {

            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _campagnBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {
                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _campagnBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagnProfile/exportData")]
        public async Task<IResult> ExportData(CampagnSearchInput request)
        {
            var searchRequest = new CampagnRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 1000,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _campagnBusiness.GetDataForExport(searchRequest);
            return Results.Ok(resultSearch);
        }


    }
}