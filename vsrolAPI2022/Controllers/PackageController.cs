using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PackageController : BaseController
    {
        private readonly IPackageManagementBussiness _business;
        private readonly IDpdManagementBussiness _dpdManagementBussiness;

        public PackageController(
            IPackageManagementBussiness linetemp,
            IDpdManagementBussiness dpdManagementBussiness,
            IUserBusiness userBusiness
        ) : base(userBusiness)
        {
            _business = linetemp;
            _dpdManagementBussiness = dpdManagementBussiness;
        }

        [HttpPost("~/api/package/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) || string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _business.Getbyid(inputRequest.Id);
            return Results.Ok(result);
        }

        [HttpPost("~/api/package/getAll")]
        public async Task<IResult> getAll(PackageRequestALl request)
        {
            var user = GetCurrentUser();

            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }
            var searchRequest = new PackageRequest()
            {
                UserId = user.Id,
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = vendorId,
                From = request.From
            };
            var resultSearch = await _business.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [HttpPost("~/api/package/GetALlInfo")]
        public async Task<IResult> GetALlInfo(PackageRequestALl request)
        {
            var user = GetCurrentUser();
            int? vendorId = null;
            if (user.RoleId == "4")
            {
                vendorId = int.Parse(user.Id);
            }
            else if (user.RoleId == "1")
            {
                vendorId = user.VendorId;
            }
            var listobject = new List<PackageIndexViewModel>();
            var searchRequest = new PackageRequest() { UserId = user.Id, VendorId = vendorId };
            var resultSearch = await _business.GetALlInfo(searchRequest);

            if (resultSearch == null)
            {

                return Results.Ok(listobject);

            }
            var dataList = resultSearch.Data as List<PackageIndexModel>;
            int dpdMax = -1;
            int dpdMin = -1;

            foreach (var itemPackage in dataList)
            {
                var packageFile = await _business.Getbyid(itemPackage.Id);

                var valueRangDpd = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DPDItem>>(
                    packageFile.Value
                );
                foreach (var item in valueRangDpd)
                {
                    var dpdItem = await _dpdManagementBussiness.GetByIdAsync(item.value);
                    if (dpdItem == null)
                    {
                        continue;
                    }

                    dpdMax = -1;
                    dpdMin = -1;

                    if (dpdMin < 0)
                    {
                        dpdMin = dpdItem.Min.Value;
                    }
                    if (dpdMax < dpdItem.Max)
                    {
                        dpdMax = dpdItem.Max.Value;
                    }

                    if (dpdItem.Min < dpdMin)
                    {
                        dpdMin = dpdItem.Min.Value;
                    }
                    if (dpdItem.Max > dpdMax)
                    {
                        dpdMax = dpdItem.Max.Value;
                    }
                }

                var totalInfo1 = await _business.GetCountBYMinMax(
                 new GetCountBYMinMaxRequest()
                 {
                     Min = dpdMin,
                     Max = dpdMax,
                     UserId = user.Id,
                     VendorId = vendorId
                 }
                 );

                var elementItem = new PackageIndexViewModel
                {
                    Name = itemPackage.Name,
                    Id = itemPackage.Id,
                    Total = totalInfo1.Total,
                    Remain = totalInfo1.Remain,
                    Active = false
                };
                listobject.Add(elementItem);

            }
            var isSetIsActive = false;

            foreach (var item in listobject)
            {
                if (isSetIsActive == true)
                {
                    continue;
                }
                if (item.Remain == 0)
                {
                    item.Active = false;
                }

                else
                {
                    item.Active = true;
                    isSetIsActive = true;
                }

            }
            return Results.Ok(
                listobject
            );
        }

        [HttpPost("~/api/package/add")]
        public async Task<IResult> Add(Package employeeAdd)
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
            if (employeeAdd.Type == 1)
            {
                employeeAdd.IdUser = string.Empty;
            }
            var itemInsert = new Package()
            {
                Name = employeeAdd.Name,
                Value = employeeAdd.Value,
                IdUser = employeeAdd.IdUser,
                Status = 0,
                Type = employeeAdd.Type,
                VendorId = vendorId,
                CreatedBy = user.Id,
                Priorities = employeeAdd.Priorities

            };
            var result = await _business.Add(itemInsert);
            return Results.Ok(result);
        }

        [HttpPost("~/api/package/update")]
        public async Task<IResult> Update(Package request)
        {
            var user = GetCurrentUser();
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
            accoutUpdate.Value = request.Value;
            accoutUpdate.IdUser = request.IdUser;
            accoutUpdate.Priorities = request.Priorities;
            var result = await _business.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        [HttpPost("~/api/package/delete")]
        public async Task<IResult> Delete(DeleteModelRequest request)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var accoutDelete = await _business.GetByIdAsync(request.Id);
            if (accoutDelete == null)
            {
                return Results.Ok(
                    new ErrorReponse()
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Success = false,
                        Message = _message.CommonError_NotFound
                    }
                );
            }
            var result = _business.Delete(accoutDelete);
            return Results.Ok(result);
        }
    }
}
