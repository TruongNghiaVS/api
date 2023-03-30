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
    public class GroupReasonController : BaseController
    {

        private readonly IGroupResonBussiness _groupReasonBusiness;
        public GroupReasonController(IGroupResonBussiness GroupReasonBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _groupReasonBusiness = GroupReasonBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _groupReasonBusiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/getAll")]
        public async Task<IResult> getAll(GroupReasonSearchInput request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }

            var searchRequest = new GroupReasonRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = vendorId,
                From = request.From

            };
            var resultSearch = await _groupReasonBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }



        [HttpPost("~/api/groupReason/getAllStatus")]
        public async Task<IResult> getAllStatus()
        {
            var user = GetCurrentUser();
            int? vendorId = null;

            if (user.RoleId == "4" || user.RoleId == "5" || user.RoleId == "3")
            {
                vendorId = user.VendorId;
            }

            var searchRequest = new GroupReasonRequest()
            {
                UserId = "1",

                VendorId = vendorId,


            };
            var resultSearch = await _groupReasonBusiness.getAllStatus(vendorId, int.Parse(user.Id));
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/add")]
        public async Task<IResult> Add(GroupReasonAdd employeeAdd)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }

            if (string.IsNullOrEmpty(employeeAdd.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }

            if (string.IsNullOrEmpty(employeeAdd.FullName))
            {
                return Results.BadRequest("Không có thông tin tên");
            }

            var resultcheck = await _groupReasonBusiness.CheckDuplicate(employeeAdd.Code);
            if (resultcheck == true)
            {
                return Results.BadRequest("Duplicate");
            }

            var account = new GroupReason()
            {
                Code = employeeAdd.Code,
                FullName = employeeAdd.FullName,
                Status = employeeAdd.Status,
                Folder = employeeAdd.Folder,
                VendorId = vendorId,
                Description = employeeAdd.Description,
                CreatedBy = "1"
            };
            var result = await _groupReasonBusiness.AddAsync(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/update")]
        public async Task<IResult> Update(GroupReasonUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            if (string.IsNullOrEmpty(request.FullName))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var accoutUpdate = await _groupReasonBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.FullName = request.FullName;
            accoutUpdate.Description = request.Description;
            accoutUpdate.Folder = request.Folder;
            accoutUpdate.Status = request.Status;



            var result = await _groupReasonBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {

            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _groupReasonBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {

                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _groupReasonBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupReason/exportData")]
        public async Task<IResult> ExportData(GroupReasonSearchInput request)
        {
            var searchRequest = new GroupReasonRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 1000,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _groupReasonBusiness.GetDataForExport(searchRequest);
            return Results.Ok(resultSearch);
        }


    }
}