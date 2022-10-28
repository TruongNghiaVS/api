﻿
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
                GroupStatus = employeeAdd.GroupStatus,
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
            accoutUpdate.Status = request.Status == 1;
            accoutUpdate.BeginTime = request.BeginTime;
            accoutUpdate.EndTime = request.EndTime;
            accoutUpdate.Priority = request.Priority;
            accoutUpdate.UpdatedBy = "1";
            accoutUpdate.GroupStatus = request.GroupStatus;

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

        private string ReadvalueStringExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Value != null)
                {
                    return cellRange.Value.ToString();
                }

            }
            return "";

        }

        private float ReadvaluefloatExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange == null)
            {
                return 0;
            }

            if (cellRange.Value == null)

            {

                return 0;
            }
            var valueCell = cellRange.Value.ToString();

            float b1 = 0;
            if (!float.TryParse(valueCell, out b1))
            {

            }
            return b1;
        }

        private int ReadvalueintExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange == null)
            {
                return 0;
            }

            if (cellRange.Value == null)

            {

                return 0;
            }
            var valueCell = cellRange.Value.ToString();

            int b1 = 0;
            if (!int.TryParse(valueCell, out b1))
            {

            }
            return b1;
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
            List<ProfileHandler> profileList = new List<ProfileHandler>();
            using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    int totalRows = workSheet.Dimension.Rows;

                    for (int i = 2; i <= totalRows; i++)
                    {
                        if (i < 7)
                        {
                            continue;
                        }

                        var orginial = ReadvaluefloatExcel(workSheet, i, 24);
                        profileList.Add(new ProfileHandler
                        {
                            CustomerName = ReadvalueStringExcel(workSheet, i, 2),
                            NoAgreement = ReadvalueStringExcel(workSheet, i, 3),
                            DayOfBirth = DateTime.Now,
                            NationalId = ReadvalueStringExcel(workSheet, i, 5),
                            MobilePhone = ReadvalueStringExcel(workSheet, i, 6),
                            Phone1 = ReadvalueStringExcel(workSheet, i, 7),
                            HouseNumber = ReadvalueStringExcel(workSheet, i, 8),
                            OfficeNumber = ReadvalueStringExcel(workSheet, i, 9),
                            OtherPhone = ReadvalueStringExcel(workSheet, i, 10),
                            Email = ReadvalueStringExcel(workSheet, i, 11),
                            Road = ReadvalueStringExcel(workSheet, i, 12),
                            SuburbanDir = ReadvalueStringExcel(workSheet, i, 13),
                            Provice = ReadvalueStringExcel(workSheet, i, 14),
                            Road1 = ReadvalueStringExcel(workSheet, i, 15),
                            SuburbanDir1 = ReadvalueStringExcel(workSheet, i, 16),
                            Provice1 = ReadvalueStringExcel(workSheet, i, 17),
                            Road2 = ReadvalueStringExcel(workSheet, i, 18),
                            SuburbanDir2 = ReadvalueStringExcel(workSheet, i, 19),
                            Provice2 = ReadvalueStringExcel(workSheet, i, 20),
                            StatusPayMent = ReadvalueStringExcel(workSheet, i, 21),
                            DPD = ReadvalueStringExcel(workSheet, i, 22),
                            RegisterDay = DateTime.Now,
                            DebitOriginal = ReadvaluefloatExcel(workSheet, i, 24),
                            AmountLoan = ReadvaluefloatExcel(workSheet, i, 25),
                            EMI = ReadvaluefloatExcel(workSheet, i, 26),
                            CampaignId = 1,
                            Assignee = "-1",
                            TotalFines = ReadvaluefloatExcel(workSheet, i, 27),
                            TotalMoneyPaid = ReadvaluefloatExcel(workSheet, i, 28),
                            Tenure = ReadvalueintExcel(workSheet, i, 29),
                            NoTenure = ReadvalueintExcel(workSheet, i, 30),
                            TotalPaid = ReadvaluefloatExcel(workSheet, i, 31),
                            LastPaid = ReadvalueintExcel(workSheet, i, 32),
                            LastPadDay = DateTime.Now,
                            NameProduct = ReadvalueStringExcel(workSheet, i, 34),
                            CodeProduct = ReadvalueStringExcel(workSheet, i, 35),
                            PriceProduct = ReadvalueStringExcel(workSheet, i, 36),
                            NoteFirstTime = ReadvalueStringExcel(workSheet, i, 37)

                        });
                    }

                }




            }
            var reqeustImport = new CampanginDataImportRequest();
            reqeustImport.ListData = profileList;
            reqeustImport.Id = request.Id;
            await _campagnBusiness.HandleImport(reqeustImport);
            return Results.Ok();

        }


        [AllowAnonymous]
        [HttpPost("~/api/campagn/assignes")]
        public async Task<IResult> Assignees(CampanginAssigneeRequest request)
        {
            var listData = request.DataRequest;
            var id = request.CampangId;
            var campangin = await _campagnBusiness.Getbyid(id);
            var allCampanginProfile = await _campagnBusiness.GetALLAsiggnee(new GetAllProfileByCampang()
            {
                Id = id,
                Limit = 100,
                Status = 0
            });
            var dataAssignee = allCampanginProfile;
            foreach (var item in listData)
            {
                var numberAssigee = item.SumCounted;

                for (int i = 0; i < item.SumCounted; i++)
                {
                    var yz = dataAssignee.First();

                    if (yz != null)
                    {
                        yz.Assignee = item.Id;
                        await _campagnBusiness.UpdateProfile(yz);
                    }

                    dataAssignee.Remove(yz);
                }
            }

            return Results.Ok();

        }

        [AllowAnonymous]
        [HttpPost("~/api/campagn/getAllCampangeAssigess")]
        public async Task<IResult> GetAllCampangeAssigess(CampagnSearchInput request)
        {
            //var user = GetCurrentUser();
            var searchRequest = new CampagnRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                CampaignId = request.CampaignId,
                From = request.From
            };

            var resultSearch = await _campagnBusiness.GetAllAsiggeeByCampagnId(searchRequest);
            return Results.Ok(resultSearch);
        }





    }
}