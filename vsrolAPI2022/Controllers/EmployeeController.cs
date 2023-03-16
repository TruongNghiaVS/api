using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.core.Utilities;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class EmployeeController : BaseController
    {

        private readonly IEmployeeBusiness _employeeBusiness;
        public EmployeeController(IEmployeeBusiness employeeBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _employeeBusiness = employeeBusiness;
        }


        [HttpPost("~/api/employee/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _employeeBusiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);
        }



        [HttpPost("~/api/employee/getAll")]
        public async Task<IResult> getAll(EmployeeSearchInput request)
        {
            var user = GetCurrentUser();

            int? VendorId = null;

            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }

            var searchRequest = new EmployeeSearchRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = VendorId,
                From = request.From

            };
            var resultSearch = await _employeeBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/employee/add")]
        public async Task<IResult> Add(EmployeeAdd employeeAdd)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(employeeAdd.UserName))
            {
                return Results.BadRequest("Không có thông tin tên đăng nhập");
            }

            if (string.IsNullOrEmpty(employeeAdd.Pass))
            {
                return Results.BadRequest("Không có thông tin mật khẩu");
            }
            if (string.IsNullOrEmpty(employeeAdd.FullName))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var resultcheck = await _employeeBusiness.CheckDuplicate(employeeAdd.UserName, "");
            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin tên đăng nhập hoặc số điện thoại");
            }
            var passNew = Utils.getMD5(employeeAdd.Pass);
            employeeAdd.CreateBy = user.CreatedBy;
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                if (employeeAdd.RoleId == "1")
                {
                    vendorId = int.Parse(user.Id);
                }
            }

            var account = new Account()
            {
                RoleId = employeeAdd.RoleId,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                FullName = employeeAdd.FullName,
                CreatedBy = user.Id,
                PhoneNumber = employeeAdd.Phone,
                Phone = employeeAdd.Phone,
                LineCode = employeeAdd.LineCode,
                Pass = passNew,
                Email = employeeAdd.Email,
                UpdatedBy = user.Id,
                IsActive = true,
                VendorId = vendorId,
                UserName = employeeAdd.UserName
            };

            var result = await _employeeBusiness.AddAsync(account);
            return Results.Ok(result);
        }

        [HttpPost("~/api/employee/changePassword")]
        public async Task<IResult> ChangePassword(EmployeeChangePassword request)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.PaswordNew))
            {
                return Results.BadRequest("Không có thông tin mật khẩu mới");
            }
            if (string.IsNullOrEmpty(request.RepeatPassword))
            {
                return Results.BadRequest("Nhập lại mật khẩu mới");
            }
            if (request.PaswordNew != request.RepeatPassword)
            {
                return Results.BadRequest("Hai mật khẩu không trùng khớp nhau");
            }

            var accoutUpdate = await _employeeBusiness.GetByIdAsync(user.Id.ToString());
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            var passwordNew = Utils.getMD5(request.PaswordNew);

            accoutUpdate.Pass = passwordNew;

            var ressultPass = await _employeeBusiness.UpdatePass(user.Id, passwordNew);

            return Results.Ok(ressultPass);
        }

        [HttpPost("~/api/employee/ResetPassword")]
        public async Task<IResult> ResetPassword(EmployeeChangePassword request)
        {
            if (string.IsNullOrEmpty(request.UserId))
            {
                return Results.BadRequest("Không có thông tin người dùng");
            }
            if (string.IsNullOrEmpty(request.PaswordNew))
            {
                return Results.BadRequest("Không có thông tin mật khẩu mới");
            }
            if (string.IsNullOrEmpty(request.RepeatPassword))
            {
                return Results.BadRequest("Nhập lại mật khẩu mới");
            }
            if (request.PaswordNew != request.RepeatPassword)
            {
                return Results.BadRequest("Hai mật khẩu không trùng khớp nhau");
            }

            var accoutUpdate = await _employeeBusiness.GetByIdAsync(request.UserId);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            var passwordNew = Utils.getMD5(request.PaswordNew);

            accoutUpdate.Pass = passwordNew;

            var ressultPass = await _employeeBusiness.UpdatePass(request.UserId, passwordNew);

            return Results.Ok(ressultPass);
        }


        [HttpPost("~/api/employee/update")]
        public async Task<IResult> Update(EmployeeUpdate request)
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
            //if (string.IsNullOrEmpty(request.LineCode))
            //{
            //    return Results.BadRequest("Không có thông mã gọi");
            //}

            var accoutUpdate = await _employeeBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            if (request.RoleId == "4")
            {
                accoutUpdate.VendorId = int.Parse(accoutUpdate.Id);
            }
            accoutUpdate.RoleId = request.RoleId;
            accoutUpdate.FullName = request.FullName;
            accoutUpdate.Phone = request.Phone;
            accoutUpdate.Email = request.Email;
            accoutUpdate.IsActive = request.Status == 1;
            accoutUpdate.UpdatedBy = user.Id;
            if (!string.IsNullOrEmpty(request.LineCode))
            {
                accoutUpdate.LineCode = request.LineCode;
            }

            var result = await _employeeBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }



        [HttpPost("~/api/employee/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {
            //var user = GetCurrentUser();

            //return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _employeeBusiness.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {

                return Results.Ok(new ErrorReponse()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = false,
                    Message = _message.CommonError_NotFound

                });
            }
            var result = _employeeBusiness.Delete(accoutDelete);
            return Results.Ok(result);
        }



        [HttpPost("~/api/employee/exportData")]
        public async Task<IResult> ExportData(EmployeeSearchInput request)
        {

            var searchRequest = new EmployeeSearchRequest()
            {
                UserId = "1",
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 1000,
                To = request.To,
                From = request.From

            };
            var resultSearch = await _employeeBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }






    }
}