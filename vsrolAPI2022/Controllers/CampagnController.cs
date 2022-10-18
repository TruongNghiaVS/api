using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using VS.core.API.Error.Model;
using VS.core.API.model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;


namespace vsrolAPI2022.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class CampagnController : BaseController
    {

        private readonly ICampagnBussiness _campagnBusiness;
        public CampagnController(ICampagnBussiness campagnBusiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagn/getById")]
        public async Task<IResult> GetById(InputIdRequest inputRequest)
        {
            if (string.IsNullOrEmpty(inputRequest.Id) ||
                string.IsNullOrEmpty(inputRequest.Id))
            {
                return Results.BadRequest(_message.CommonError_ErrorRequestInput);
            }
            var result = await _campagnBusiness.Getbyid(inputRequest.Id);
            return Results.Ok(result);

        }


        [AllowAnonymous]
        [HttpPost("~/api/campagn/getAll")]
        public async Task<IResult> getAll(CampagnSearchInput request)
        {
            //var user = GetCurrentUser();
            var searchRequest = new CampagnRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                From = request.From
            };
            var resultSearch = await _campagnBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagn/add")]
        public async Task<IResult> Add(CampagnAdd employeeAdd)
        {
            var user = GetCurrentUser();
            if (string.IsNullOrEmpty(employeeAdd.Code))
            {
                return Results.BadRequest("Không có thông tin mã code");
            }
            var resultcheck = await _campagnBusiness.CheckDuplicate(employeeAdd.Code);

            if (resultcheck == true)
            {
                return Results.BadRequest("Bị trùng thông tin tên đăng nhập hoặc số điện thoại");
            }
            var account = new Campagn()
            {
                Code = employeeAdd.Code,
                CompanyId = "-1",
                DisplayName = employeeAdd.DisplayName,
                Status = true,
                SumCount = employeeAdd.SumCount,
                ProcessingCount = employeeAdd.ProcessingCount,
                ClosedCount = employeeAdd.ClosedCount,
                BeginTime = employeeAdd.BeginTime,
                EndTime = employeeAdd.EndTime,
                Priority = employeeAdd.Priority,
                ShortDes = employeeAdd.ShortDes
            };
            var result = await _campagnBusiness.AddAsync(account);
            return Results.Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("~/api/campagn/update")]
        public async Task<IResult> Update(CampagnUpdate request)
        {
            //var user = GetCurrentUser();
            if (string.IsNullOrEmpty(request.Id))
            {
                return Results.BadRequest("Không có thông tin ID");
            }

            var accoutUpdate = await _campagnBusiness.GetByIdAsync(request.Id);
            if (accoutUpdate == null)
            {
                return Results.BadRequest("Không có thông tin profile tương ứng");
            }

            accoutUpdate.DisplayName = request.DisplayName;

            accoutUpdate.UpdatedBy = "1";
            accoutUpdate.ShortDes = request.ShortDes;
            accoutUpdate.SumCount = request.SumCount;
            accoutUpdate.ProcessingCount = request.ProcessingCount;
            accoutUpdate.ClosedCount = request.ClosedCount;
            accoutUpdate.Status = request.Status;
            accoutUpdate.BeginTime = request.BeginTime;
            accoutUpdate.EndTime = request.EndTime;
            accoutUpdate.Priority = request.Priority;
            accoutUpdate.UpdatedBy = "1";
            var result = await _campagnBusiness.UpdateAsyn(accoutUpdate);
            return Results.Ok(result);
        }

        //[Authorize]
        //[HttpPost("~/employee/delete")]

        [AllowAnonymous]
        [HttpPost("~/api/campagn/delete")]
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
        [HttpPost("~/api/campagn/exportData")]
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


        [AllowAnonymous]
        [HttpPost("~/api/campagn/importDataById")]
        public async Task<IResult> ImportData([FromForm] CampanginDataImport request)
        {

            var fileRequest = request.FileData;
            if (fileRequest == null || fileRequest.Count == 0)
            {
                return Results.BadRequest("No error report");
            }
            var fileHandler = fileRequest.FirstOrDefault();
            if (fileHandler == null)
            {
                return Results.BadRequest("No error report");
            }
            using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    int totalRows = workSheet.Dimension.Rows;
                    List<ProfileHandler> profileList = new List<ProfileHandler>();
                    for (int i = 2; i <= totalRows; i++)
                    {
                        if (i < 7)
                        {
                            continue;
                        }
                        profileList.Add(new ProfileHandler
                        {
                            CustomerName = workSheet.Cells[i, 2] != null ? workSheet.Cells[i, 5].Value.ToString() : "",
                            NoAgreement = workSheet.Cells[i, 3] != null ? workSheet.Cells[i, 5].Value.ToString() : "",
                            DayOfBirth = DateTime.Now,
                            NationalId = workSheet.Cells[i, 5] != null ? workSheet.Cells[i, 5].Value.ToString() : "",

                        });
                    }
                }
            }
            return Results.Ok();
        }

    }
}