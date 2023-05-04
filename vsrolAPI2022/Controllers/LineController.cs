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
    public class LineController : BaseController
    {

        private readonly ILineManagementBussiness _business;
        public LineController(ILineManagementBussiness linetemp,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _business = linetemp;
        }


        [HttpPost("~/api/line/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _business.Getbyid(inputRequest.Id);
            return Results.Ok(result);
        }


        [HttpPost("~/api/line/getAll")]
        public async Task<IResult> getAll(LineSearchInput request)
        {
            var user = GetCurrentUser();

            var searchRequest = new LineManagementRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                lineIdAdd = request.lineIdAdd,
                To = request.To,
                VendorId = user.VendorId != null ? user.VendorId : null,
                Userid = int.Parse(user.Id),
                From = request.From

            };
            var resultSearch = await _business.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }




        [HttpPost("~/api/line/add")]
        public async Task<IResult> Add(LineAdd employeeAdd)
        {
            var user = GetCurrentUser();

            if (string.IsNullOrEmpty(employeeAdd.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }

            if (string.IsNullOrEmpty(employeeAdd.Name))
            {
                return Results.BadRequest("Không có thông tin tên");
            }

            var resultcheck = await _business.CheckDuplicate(employeeAdd.Code);
            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin tên đăng nhập hoặc số điện thoại");
            }

            var account = new Line()
            {
                Code = employeeAdd.Code,
                Name = employeeAdd.Name,
                Status = 2,

                VendorId = user.VendorId,
                CreatedBy = user.CreatedBy


            };
            var result = await _business.AddAsync(account);
            return Results.Ok(result);
        }


        [HttpPost("~/api/line/update")]
        public async Task<IResult> Update(LineUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var accoutUpdate = await _business.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.Name = request.Name;
            accoutUpdate.LineId = request.LineId;
            accoutUpdate.Status = request.Status;
            var result = await _business.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]


        [HttpPost("~/api/line/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {

            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _business.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {

                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _business.Delete(accoutDelete);
            return Results.Ok(result);
        }




    }
}