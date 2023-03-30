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
    public class GroupEmployeeController : BaseController
    {

        private readonly IGroupEmpBussiness _groupEmpBussiness;
        public GroupEmployeeController(IGroupEmpBussiness groupEmpBussiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _groupEmpBussiness = groupEmpBussiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupEmployee/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }

            var result = await _groupEmpBussiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);
        }

        [HttpPost("~/api/groupEmployee/getAll")]
        public async Task<IResult> getAll(GroupEmployeeRequest request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                request.VendorId = vendorId;
            }
            var searchRequest = new GroupEmployeeRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = request.VendorId,
                From = request.From,
                Type = request.Type

            };

            var resultSearch = await _groupEmpBussiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/groupEmployee/getAllMemberHaveNotIngroup")]
        public async Task<IResult> getAllMemberHaveNotIngroup(GroupEmployeeHaveNotInGroupRequest request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                request.VendorId = vendorId;
            }
            var searchRequest = new GroupEmployeeHaveNotInGroupRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = request.VendorId,
                From = request.From,


            };
            var resultSearch = await _groupEmpBussiness.GetAllMeberHaveNotGroup(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/groupEmployee/getMemberByGroup")]
        public async Task<IResult> getMemberByGroup(MemberGroupByIdRequest request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                request.VendorId = vendorId;
            }
            var searchRequest = new MemberGroupByIdRequest()
            {
                VendorId = request.VendorId,
                GroupId = request.GroupId

            };
            var resultSearch = await _groupEmpBussiness.getMemberByGroup(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/groupEmployee/add")]
        public async Task<IResult> Add(GroupEmployeeAdd input)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(input.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }
            if (string.IsNullOrEmpty(input.Name))
            {
                return Results.BadRequest("Không có thông tin tên");
            }

            var resultcheck = await _groupEmpBussiness.CheckDuplicate(input.Code);
            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin mã code");
            }

            var itemInsert = new GroupEmployee()
            {
                Code = input.Code,
                Name = input.Name,
                IsActive = input.IsActive == 1,
                CreatedBy = user.Id,
                ManagerId = input.ManagerId
            };

            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                itemInsert.VendorId = vendorId;
            }

            var result = await _groupEmpBussiness.AddAsync(itemInsert);
            return Results.Ok(result);
        }

        [HttpPost("~/api/groupEmployee/AddMember")]
        public async Task<IResult> AddMember(AddMemberGroupRequest input)
        {
            var user = GetCurrentUser();
            if (!input.Groupid.HasValue)
            {
                return Results.BadRequest("Không có thông tin nhóm");
            }
            if (!input.Memberid.HasValue)
            {
                return Results.BadRequest("Không có thông tin nhân viên");
            }
            var itemInsert = new GroupMember()
            {
                Groupid = input.Groupid,
                Memberid = input.Memberid,
            };
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                itemInsert.VendorId = vendorId;
            }
            var result = await _groupEmpBussiness.AddMemberGroup(itemInsert);
            return Results.Ok(result);
        }
        [HttpPost("~/api/groupEmployee/DeleteMember")]
        public async Task<IResult> DeleteMember(DeleteModelRequest input)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(input.Id))
            {
                return Results.BadRequest("Không có thông tin cần xóa");
            }


            var result = await _groupEmpBussiness.Deletemember(int.Parse(input.Id));
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/groupEmployee/update")]
        public async Task<IResult> Update(GroupEmployeeUpdate input)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(input.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }


            if (string.IsNullOrEmpty(input.Name))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var accoutUpdate = await _groupEmpBussiness.GetByIdAsync(input.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.Name = input.Name;
            accoutUpdate.UpdatedBy = user.Id;
            accoutUpdate.ManagerId = input.ManagerId;
            accoutUpdate.IsActive = input.IsActive == 1;

            var result = await _groupEmpBussiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }


        [AllowAnonymous]
        [HttpPost("~/api/groupEmployee/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {

            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _groupEmpBussiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {

                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _groupEmpBussiness.Delete(accoutDelete);
            return Results.Ok(result);
        }



        [HttpPost("~/api/groupEmployee/getAllManager")]
        public async Task<IResult> getAllManager(GroupEmployeeRequest request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
                request.VendorId = vendorId;
            }
            var resultSearch = await _groupEmpBussiness.GetAllManager(request);
            return Results.Ok(resultSearch);
        }


    }
}