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
    public class MasterDataNewController : BaseController
    {

        private readonly IMasterDataNewBussiness _masterDataBusiness;
        public MasterDataNewController(IMasterDataNewBussiness masterDataBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _masterDataBusiness = masterDataBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdataNew/getById")]
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



        [HttpPost("~/api/masterdataNew/getAll")]
        public async Task<IResult> getAll(MasterDataNewSearchInput request)
        {
            var user = GetCurrentUser();

            var searchRequest = new MaterDataNewRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                From = request.From,
                Type = request.Type

            };
            var resultSearch = await _masterDataBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdataNew/add")]
        public async Task<IResult> Add(MasterDataNewAdd employeeAdd)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(employeeAdd.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }
            if (employeeAdd.Type < 1)
            {
                return Results.BadRequest("Không có thông tin loại nhóm");
            }

            if (string.IsNullOrEmpty(employeeAdd.Name))
            {
                return Results.BadRequest("Không có thông tin tên");
            }

            var resultcheck = await _masterDataBusiness.CheckDuplicate(employeeAdd.Code);
            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin mã code");
            }

            var itemInsert = new MasterDataNew()
            {
                Code = employeeAdd.Code,
                Name = employeeAdd.Name,
                Type = employeeAdd.Type,
                Status = employeeAdd.Status,
                CreatedBy = user.Id
            };

            var result = await _masterDataBusiness.Add(itemInsert);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/masterdataNew/update")]
        public async Task<IResult> Update(MasterDataNewUpdate request)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                return Results.BadRequest("Không có thông tin họ tên");
            }

            var accoutUpdate = await _masterDataBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }
            accoutUpdate.Name = request.Name;
            accoutUpdate.UpdatedBy = user.Id;

            var result = await _masterDataBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/masterdataNew/delete")]
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

        [HttpPost("~/api/masterdataNew/getAllInfo")]
        public async Task<IResult> getAllInfo(MasterDataNewSearchInput request)
        {
            var user = GetCurrentUser();

            var searchRequest = new MaterDataNewRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                From = request.From,
                Type = request.Type
            };

            var resultSearch = await _masterDataBusiness.GetInfo(searchRequest);
            return Results.Ok(resultSearch);
        }



    }
}