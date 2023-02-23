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
    public class MasterDataController : BaseController
    {

        private readonly IMasterDataBussiness _masterDataBusiness;
        public MasterDataController(IMasterDataBussiness masterDataBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _masterDataBusiness = masterDataBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdata/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }

            var result = await _masterDataBusiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);

        }



        [HttpPost("~/api/masterdata/getAll")]
        public async Task<IResult> getAll(MasterDataSearchInput request)
        {

            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }
            var searchRequest = new MaterDataRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                From = request.From,
                GroupStatus = request.GroupStatus
            };
            var resultSearch = await _masterDataBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdata/add")]
        public async Task<IResult> Add(MasterDataAdd employeeAdd)
        {
            var user = GetCurrentUser();



            if (string.IsNullOrEmpty(employeeAdd.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }

            if (!employeeAdd.GroupId.HasValue)
            {
                return Results.BadRequest("Không có thông tin nhóm");
            }


            if (string.IsNullOrEmpty(employeeAdd.FullName))
            {
                return Results.BadRequest("Không có thông tin tên");
            }
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id.ToString());
            }
            var resultcheck = await _masterDataBusiness.CheckDuplicate(employeeAdd.Code, vendorId != null ? vendorId.ToString() : null);

            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin tên đăng nhập hoặc số điện thoại");
            }



            var account = new MasterData()
            {
                Code = employeeAdd.Code,
                FullName = employeeAdd.FullName,
                DisplayName = employeeAdd.DisplayName,
                CreatedBy = user.Id,
                UpdatedBy = user.Id,
                Hour = employeeAdd.Hour,
                Day = employeeAdd.Hour,
                GroupId = employeeAdd.GroupId,
                VendorId = vendorId
            };
            var result = await _masterDataBusiness.AddAsync(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdata/update")]
        public async Task<IResult> Update(MasterDataUpdate request)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            if (string.IsNullOrEmpty(request.FullName))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var accoutUpdate = await _masterDataBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.FullName = request.FullName;
            accoutUpdate.DisplayName = request.DisplayName;

            accoutUpdate.UpdatedBy = user.Id;
            accoutUpdate.Hour = request.Hour;
            accoutUpdate.Day = request.Day;

            var result = await _masterDataBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/masterdata/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {
            //var user = GetCurrentUser();

            //return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _masterDataBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {

                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _masterDataBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdata/exportData")]
        public async Task<IResult> ExportData(MasterDataSearchInput request)
        {
            var searchRequest = new MaterDataRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 1000,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _masterDataBusiness.GetDataForExport(searchRequest);



            return Results.Ok(resultSearch);
        }


    }
}