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
    public class DpdController : BaseController
    {

        private readonly IDpdManagementBussiness _business;
        public DpdController(IDpdManagementBussiness linetemp,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _business = linetemp;
        }


        [HttpPost("~/api/dpd/getById")]
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


        [HttpPost("~/api/dpd/getAll")]
        public async Task<IResult> getAll(DPDRequest request)
        {
            var user = GetCurrentUser();

            if (user.RoleId == "4")
            {
                request.VendorId = int.Parse(user.Id);
            }
            var searchRequest = new DPDRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = request.VendorId,
                From = request.From

            };
            var resultSearch = await _business.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/dpd/getAllPackage")]
        public async Task<IResult> GetAllPackageUser(DPDRequest request)
        {
            var user = GetCurrentUser();

            if (user.RoleId == "4")
            {
                request.VendorId = int.Parse(user.Id);

            }
            var searchRequest = new DPDRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = request.VendorId,
                From = request.From

            };
            var resultSearch = await _business.GetALl(searchRequest);

            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/dpd/add")]
        public async Task<IResult> Add(Dpd employeeAdd)
        {
            var user = GetCurrentUser();

            if (string.IsNullOrEmpty(employeeAdd.Name))
            {
                return Results.BadRequest("Không có thông tin tên");
            }
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }
            var itemInsert = new Dpd()
            {
                Name = employeeAdd.Name,
                Min = employeeAdd.Min,
                Max = employeeAdd.Max,
                Status = 0,
                VendorId = vendorId,
                CreatedBy = user.Id
            };
            var result = await _business.Add(itemInsert);
            return Results.Ok(result);
        }




        [HttpPost("~/api/dpd/update")]
        public async Task<IResult> Update(Dpd request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Không có thông tin  tên");
            }

            var accoutUpdate = await _business.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.Name = request.Name;
            accoutUpdate.Min = request.Min;
            accoutUpdate.Max = request.Max;
            var result = await _business.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        [HttpPost("~/api/dpd/delete")]
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