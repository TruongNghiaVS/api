
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
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
    public class StoreController : BaseController
    {

        private readonly IStoreBussiness _campagnBusiness;

        private readonly ISkipInfoBussiness _business;
        public StoreController(IStoreBussiness campagnBusiness,
            ISkipInfoBussiness skipInfoBussiness,
            IUserBusiness userBusiness) : base(userBusiness)
        {
            _campagnBusiness = campagnBusiness;
            _business = skipInfoBussiness;
        }
        
        
        [HttpPost("~/api/store/getall")]
        public async Task<IResult> Getall(CampagnProfileSearchInput request)
        {
            var user = GetCurrentUser();
            int? VendorId = null;
            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }
            if (user.RoleId == "4")
            {
                VendorId = int.Parse(user.Id);
            }
            else if (user.RoleId == "2")
            {

            }
            else
            {
                VendorId = user.VendorId;
            }


            var infoSearch = new GetAllProfileByCampang()
            {
                Token = request.Token,
                Status = request.Status,
                Page = request.Page,
                Limit = request.Limit,
                To = request.To,
              
                Id = request.Id,
                From = request.From,
                UserId = user.Id,
                ColorCode = request.ColorCode,
                VendorId = VendorId,
                PhoneSerach = request.PhoneSerach,
                LineCode = request.LineCode,
                NoAgreement = request.NoAgree,
                TypegetData = request.TypegetData,
                SkipData = request.SkipData,
                Cmnd = request.Cmnd


            };
            var resultSearch = await _campagnBusiness.GetALlProfileByCampaign(infoSearch);
            return Results.Ok(resultSearch);
          

        }


        [HttpPost("~/api/store/getInfo")]
        public async Task<IResult> getInfo(StoreSearch request)
        {
            var tokenSearch = request.Token;
            var resultSearch = await _campagnBusiness.GetInfo(tokenSearch);

              
            var reponse = new
             {
               
                Result = resultSearch.Result,
                ListSkipNew = resultSearch.ListSkipNew,
                listHistory = resultSearch.listHistory

            };
            
            return Results.Ok(reponse);

        }

        [HttpPost("~/api/store/importDataSkipInfo")]

        private DateTime? ReadvalueDateExcel2(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange != null)
                {

                    if (cellRange.Text != null)
                    {
                        try
                        {
                            var dtp = DateTime.Parse(cellRange.Text.ToString());
                            return dtp;
                            return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                        }
                        catch (Exception)
                        {
                            try
                            {
                                return DateTime.ParseExact(cellRange.Text.Trim(), "MM/dd/yyyy", null);

                            }
                            catch (Exception)
                            {
                                try
                                {
                                    var dtp = DateTime.Parse(cellRange.Text.ToString());
                                    return dtp;
                                    return null;
                                }
                                catch (Exception)
                                {

                                    return null;
                                }

                                return null;
                            }

                            return null;
                        }

                    }

                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }


        }

        private DateTime? ReadvalueDateExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
            {
                var cellRange = excelworksheet.Cells[row, col];
                if (cellRange != null)
                {

                    if (cellRange.Text != null)
                    {
                        try
                        {
                            var dtp = DateTime.Parse(cellRange.Text.ToString());
                            return dtp;
                            return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);

                        }
                        catch (Exception)
                        {

                            try
                            {
                                return DateTime.ParseExact(cellRange.Text.Trim(), "MM/dd/yyyy", null);
                            }
                            catch (Exception)
                            {

                                try
                                {
                                    var dtp = DateTime.Parse(cellRange.Text.ToString());
                                    return dtp;
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        return DateTime.ParseExact(cellRange.Text.Trim(), "dd/MM/yyyy", null);
                                    }
                                    catch (Exception)
                                    {

                                        return null;
                                    }
                                    return null;
                                }

                                return null;
                            }


                            return null;
                        }

                    }

                }
                return null;
            }
            catch (Exception)
            {

                return null;
            }



        }
        private string? ReadvaluestringExcelWidthNull(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
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
            catch (Exception)
            {

                return "";
            }

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


        private int ReadvalueintExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
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
            catch (Exception)
            {

                return 0;
            }

        }

        private float ReadvaluefloatExcel(ExcelWorksheet excelworksheet, int row, int col)
        {
            try
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
            catch (Exception)
            {

                return 0;
            }

        }

        [AllowAnonymous]
        [HttpPost("~/api/store/importDataSkipInfo")]
        public async Task<IResult> ImportDataSkipInfo([FromForm] CampanginDataImport request)
        {
            int k = 0;
            try
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
                List<StoreSkipInfo> profileList = new List<StoreSkipInfo>();
                await using (MemoryStream ms = new MemoryStream())
                {
                    await fileHandler.CopyToAsync(ms);
                    using (ExcelPackage package = new ExcelPackage(ms))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
                        int totalRows = workSheet.Dimension.Rows;
                        for (int i = 2; i <= totalRows; i++)
                        {
                            if (i < 1)
                            {
                                continue;
                            }

                            var skipInfo = new StoreSkipInfo()
                            {
                                NationalId = ReadvalueStringExcel(workSheet, i, 1),
                                TypeCustomer = ReadvalueStringExcel(workSheet, i, 2),
                                Name = ReadvalueStringExcel(workSheet, i, 3),
                                Relation = ReadvalueStringExcel(workSheet, i, 4),
                                Dob = ReadvalueDateExcel2(workSheet, i, 5),
                                CMND = ReadvalueStringExcel(workSheet, i, 6),
                                PhoneNumber = ReadvalueStringExcel(workSheet, i, 7),
                                Address = ReadvalueStringExcel(workSheet, i, 8),
                                Address2 = ReadvalueStringExcel(workSheet, i, 9),
                                FromDay = ReadvalueStringExcel(workSheet, i, 10),
                                ToDay = ReadvalueStringExcel(workSheet, i, 11),
                                Timejoin = ReadvalueStringExcel(workSheet, i, 12),
                                Position = ReadvalueStringExcel(workSheet, i, 13),
                                SalaryDe = ReadvalueStringExcel(workSheet, i, 14),
                                Hsl = ReadvalueStringExcel(workSheet, i, 15),
                                CompanyName = ReadvalueStringExcel(workSheet, i, 16),
                                WorkPlace = ReadvalueStringExcel(workSheet, i, 17),
                                WorkContact = ReadvalueStringExcel(workSheet, i, 18),
                                WhoseCompany = ReadvalueStringExcel(workSheet, i, 19),
                                PhoneCompany = ReadvalueStringExcel(workSheet, i, 20),
                                PersonContact = ReadvalueStringExcel(workSheet, i, 21),
                                PersonContactPhone = ReadvalueStringExcel(workSheet, i, 22),
                                PersonContactEmail = ReadvalueStringExcel(workSheet, i, 23),
                                CreatedBy ="1",
                                UpdatedBy ="1",
                                CreateAt =DateTime.Now,
                                UpdateAt =DateTime.Now,
                                Deleted =false,
                                NoAgree =""

                            };

                            profileList.Add(skipInfo);
                        }

                    }
                }

                await _campagnBusiness.HandleImport(profileList, null);
                
            }
            catch (Exception e)
            {

                return Results.BadRequest(k);
            }
            return Results.Ok();

        }







    }
}