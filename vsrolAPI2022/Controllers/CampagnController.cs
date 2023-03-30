
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



        [HttpPost("~/api/campagn/getAll")]
        public async Task<IResult> getAll(CampagnSearchInput request)
        {
            var user = GetCurrentUser();
            int? VendorId = null;
            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }
            var searchRequest = new CampagnRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
                VendorId = VendorId,
                From = request.From
            };
            var resultSearch = await _campagnBusiness.GetALl(searchRequest);
            return Results.Ok(resultSearch);
        }


        [HttpPost("~/api/campagn/getOverViewDashboardById")]
        public async Task<IResult> getOverViewDashboardById(CampangnOverviewByIdRequest request)
        {
            var user = GetCurrentUser();
            int? VendorId = null;
            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }
            var searchRequest = new CampangnOverviewByIdRequest()
            {
                CampaignId = request.CampaignId
            };
            var resultSearch = await _campagnBusiness.GetOverViewDashboardById(searchRequest);
            return Results.Ok(resultSearch);
        }



        [HttpPost("~/api/campagn/add")]
        public async Task<IResult> Add(CampagnAdd employeeAdd)
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
                ShortDes = employeeAdd.ShortDes,
                VendorId = vendorId

            };
            var result = await _campagnBusiness.AddAsync(account);
            return Results.Ok(result);
        }


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


        private DateTime? ReadvalueDateExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Text != null)
                {
                    try
                    {

                        return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                    }
                    catch (Exception)
                    {
                        try
                        {
                            return DateTime.Now.AddYears(-int.Parse(cellRange.Text));
                        }
                        catch (Exception)
                        {

                            return null;
                        }

                        return null;
                    }

                }

            }
            return null;

        }



        private DateTime? ReadvalueDateExcel2(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange != null)
            {

                if (cellRange.Text != null)
                {
                    try
                    {

                        return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                    }
                    catch (Exception)
                    {
                        try
                        {
                            return DateTime.Now.AddYears(-int.Parse(cellRange.Text));
                        }
                        catch (Exception)
                        {

                            return null;
                        }

                        return null;
                    }

                }

            }
            return null;

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

        private string? ReadvaluestringExcelWidthNull(ExcelWorksheet excelworksheet, int row, int col)
        {
            var cellRange = excelworksheet.Cells[row, col];
            if (cellRange == null)
            {
                return null;
            }

            if (cellRange.Value == null)

            {

                return null;
            }
            var valueCell = cellRange.Value.ToString();


            return valueCell;
        }


        [AllowAnonymous]
        [HttpPost("~/api/campagn/importDataById")]
        public async Task<IResult> ImportData([FromForm] CampanginDataImport request)
        {
            var userLogin = new Account()
            {
                Id = "1"
            };
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
            await using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    int totalRows = workSheet.Dimension.Rows;

                    for (int i = 2; i <= totalRows; i++)
                    {
                        if (i < 1)
                        {
                            continue;
                        }
                        var lastDayPad = ReadvalueDateExcel2(workSheet, i, 22);
                        if (lastDayPad == null)
                        {
                            lastDayPad = DateTime.Now;
                        }
                        var registerDate = ReadvalueDateExcel(workSheet, i, 5);
                        var doB = ReadvalueDateExcel(workSheet, i, 3);
                        string? assigneeId = null;

                        assigneeId = ReadvaluestringExcelWidthNull(workSheet, i, 40);
                        profileList.Add(new ProfileHandler
                        {
                            CustomerName = ReadvalueStringExcel(workSheet, i, 2),
                            NoAgreement = ReadvalueStringExcel(workSheet, i, 1),
                            DayOfBirth = doB,
                            NationalId = ReadvalueStringExcel(workSheet, i, 4),
                            MobilePhone = ReadvalueStringExcel(workSheet, i, 26),
                            Phone1 = ReadvalueStringExcel(workSheet, i, 28),
                            HouseNumber = ReadvalueStringExcel(workSheet, i, 27),
                            OfficeNumber = ReadvalueStringExcel(workSheet, i, 30),
                            OtherPhone = ReadvalueStringExcel(workSheet, i, 29),
                            DPD = ReadvalueStringExcel(workSheet, i, 25),
                            Email = "",

                            Road = ReadvalueStringExcel(workSheet, i, 31),
                            SuburbanDir = ReadvalueStringExcel(workSheet, i, 32),
                            Provice = ReadvalueStringExcel(workSheet, i, 33),
                            Road1 = ReadvalueStringExcel(workSheet, i, 34),
                            SuburbanDir1 = ReadvalueStringExcel(workSheet, i, 35),
                            Provice1 = ReadvalueStringExcel(workSheet, i, 36),

                            Road2 = "",
                            SuburbanDir2 = "",
                            Provice2 = "",
                            StatusPayMent = ReadvalueStringExcel(workSheet, i, 23),

                            RegisterDay = registerDate,

                            DebitOriginal = ReadvaluefloatExcel(workSheet, i, 24),
                            AmountLoan = ReadvaluefloatExcel(workSheet, i, 12),
                            EMI = ReadvaluefloatExcel(workSheet, i, 15),
                            CampaignId = int.Parse(request.Id),
                            //Assignee = "-1",
                            TotalFines = ReadvaluefloatExcel(workSheet, i, 19),
                            TotalMoneyPaid = ReadvaluefloatExcel(workSheet, i, 13),
                            Tenure = ReadvalueintExcel(workSheet, i, 14),
                            NoTenure = ReadvalueintExcel(workSheet, i, 18),
                            TotalPaid = 0,

                            LastPaid = 0,
                            LastPadDay = lastDayPad,
                            NameProduct = ReadvalueStringExcel(workSheet, i, 7),
                            CodeProduct = ReadvalueStringExcel(workSheet, i, 6),
                            PriceProduct = ReadvalueStringExcel(workSheet, i, 10),
                            NoteFirstTime = ReadvalueStringExcel(workSheet, i, 38),
                            NoteRel = ReadvalueStringExcel(workSheet, i, 39),
                            CreatedBy = userLogin.Id,
                            AssignedId = assigneeId

                        }); ;
                    }

                }




            }
            var reqeustImport = new CampanginDataImportRequest();
            reqeustImport.ListData = profileList;
            reqeustImport.Id = request.Id;
            await _campagnBusiness.HandleImport(reqeustImport, userLogin);
            return Results.Ok();

        }




        [AllowAnonymous]
        [HttpPost("~/api/campagn/importHistory")]
        public async Task<IResult> ImportHistory([FromForm] CampanginDataImport request)
        {
            var userLogin = new Account()
            {
                Id = "1"
            };
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
            await using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);
                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        if (i < 1)
                        {
                            continue;
                        }
                        var campangnId = "4";
                        var reasonName = ReadvalueStringExcel(workSheet, i, 40);
                        var assigneName = ReadvalueStringExcel(workSheet, i, 42);
                        var ShortDescription = ReadvalueStringExcel(workSheet, i, 43);
                        var promiseday = ReadvalueDateExcel(workSheet, i, 44);
                        var moneyPromise = ReadvalueStringExcel(workSheet, i, 45);
                        var relationship = ReadvalueStringExcel(workSheet, i, 47);
                        var noAgree = ReadvalueStringExcel(workSheet, i, 1);
                        var reasonCode = reasonName.Split('-');


                    }

                }




            }
            var reqeustImport = new CampanginDataImportRequest();
            reqeustImport.ListData = profileList;
            reqeustImport.Id = request.Id;
            await _campagnBusiness.HandleImport(reqeustImport, userLogin);
            return Results.Ok();

        }


        [AllowAnonymous]
        [HttpPost("~/api/campagn/skipInfo")]
        public async Task<IResult> SkipInfo([FromForm] CampanginDataImport request)
        {
            var userLogin = new Account()
            {
                Id = "1"
            };
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
            await using (MemoryStream ms = new MemoryStream())
            {
                await fileHandler.CopyToAsync(ms);

                using (ExcelPackage package = new ExcelPackage(ms))
                {
                    ExcelWorksheet workSheet = package.Workbook.Worksheets["Sheet1"];
                    int totalRows = workSheet.Dimension.Rows;
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var noAgree = ReadvalueStringExcel(workSheet, i, 1);
                        var fullName = ReadvalueStringExcel(workSheet, i, 2);
                        var noPhoneCustomer = ReadvalueStringExcel(workSheet, i, 5);
                        var noNationalId = ReadvalueStringExcel(workSheet, i, 3);


                        if (string.IsNullOrEmpty(noAgree) || string.IsNullOrEmpty(noNationalId))
                        {
                            continue;
                        }
                        var nameNT = ReadvalueStringExcel(workSheet, i, 10);
                        var dobNT = ReadvalueStringExcel(workSheet, i, 12);
                        var cmnNT = ReadvalueStringExcel(workSheet, i, 13);
                        var phoneNT = ReadvalueStringExcel(workSheet, i, 14);
                        var workNT = ReadvalueStringExcel(workSheet, i, 27);

                        var text = "";

                        if (!string.IsNullOrEmpty(nameNT))
                        {
                            text += "Tên người thân:" + nameNT + "\n";

                        }
                        if (!string.IsNullOrEmpty(dobNT))
                        {
                            text += "Ngày sinh NT:" + dobNT + "\n";

                        }

                        if (!string.IsNullOrEmpty(cmnNT))
                        {
                            text += "CMND NT:" + cmnNT + "\n";

                        }

                        if (!string.IsNullOrEmpty(phoneNT))
                        {
                            text += "SĐT NT:" + phoneNT + "\n";

                        }
                        if (!string.IsNullOrEmpty(workNT))
                        {
                            text += "Nơi làm việc:" + workNT + "\n";

                        }


                        var profile = await _campagnBusiness.GetProfileByNoCMND(noAgree);
                        if (profile == null)
                        {
                            continue;
                        }
                        profile.SkipContent = text;

                        await _campagnBusiness.UpdateProfile(profile);
                    }

                }
            }

            return Results.Ok();

        }





        [AllowAnonymous]
        [HttpPost("~/api/campagn/assignes")]
        public async Task<IResult> Assignees(CampanginAssigneeRequest request)
        {
            var listData = request.DataRequest;
            var sumRequestAssigee = listData.Sum(x => x.SumCounted);
            var id = request.CampangId;
            var campangin = await _campagnBusiness.Getbyid(id);
            bool isBreak = false;
            int totalAssigee = 0;
            do
            {
                if (listData.Sum(x => x.SumCounted) < 1)
                {
                    isBreak = true;
                }
                var allCampanginProfile = await _campagnBusiness.GetALLAsiggnee(new GetAllProfileByCampang()
                {
                    Id = id,
                    Limit = 100,
                    Status = 10,
                    TypegetData = "10"
                });

                if (allCampanginProfile.Count < 1)
                {
                    isBreak = true;
                }
                var dataAssignee = allCampanginProfile;
                int? totalSum = 0;
                foreach (var item in listData)
                {

                    if (dataAssignee.Count < 1)
                    {
                        continue;
                    }
                    totalSum += item.SumCounted;

                    while (item.SumCounted > 0)
                    {

                        totalAssigee++;
                        if (item.SumCounted == 1)
                        {
                            totalAssigee = totalAssigee + 0;
                        }

                        if (dataAssignee.Count < 1)
                        {
                            break;
                        }
                        var yz = dataAssignee.First();
                        if (yz != null)
                        {
                            yz.Assignee = item.Id;
                            yz.Status = 0;
                            yz.Skipp = false;
                            yz.UpdatedBy = "1";
                            await _campagnBusiness.UpdateProfile(yz);
                        }
                        else
                        {
                            break;
                        }
                        item.SumCounted--;
                        dataAssignee.Remove(yz);
                    }

                }
                if (totalSum < 1)
                {
                    isBreak = true;
                }
            }
            while (!isBreak);
            return Results.Ok();




        }


        [HttpPost("~/api/campagn/getAllCampangeAssigess")]
        public async Task<IResult> GetAllCampangeAssigess(CampagnSearchInput request)
        {
            var userLogin = GetCurrentUser();

            int? VendorId = null;

            if (userLogin.RoleId == "4")
            {
                VendorId = int.Parse(userLogin.Id);
            }
            //var user = GetCurrentUser();
            var searchRequest = new CampagnRequest()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = 100,
                To = request.To,
                VendorId = VendorId,
                CampaignId = request.CampaignId,
                From = request.From
            };

            var resultSearch = await _campagnBusiness.GetAllAsiggeeByCampagnId(searchRequest);
            return Results.Ok(resultSearch);
        }


        [AllowAnonymous]
        [HttpPost("~/api/campagn/UpdateOverViewAllCampagn")]
        public async Task<IResult> updateOverViewCampagn(CampagnSearchInput request)
        {

            var resultSearch = await _campagnBusiness.UpdateOverViewAllCampagn();
            return Results.Ok(resultSearch);
        }
        [AllowAnonymous]
        [HttpGet("~/api/campagn/RecallCase")]
        public async Task<ActionResult> ResetCase()
        {
            await _campagnBusiness.ResetCase();

            return Ok(true);

        }





    }
}