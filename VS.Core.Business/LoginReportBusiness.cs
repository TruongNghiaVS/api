
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using VS.core.Report.Model;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class LoginReportBusiness : BaseBusiness, ILoginReportBussiness
    {
        public LoginReportBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> Add(LoginReport entity)
        {
            return _unitOfWork.LoginRe.Add(entity);
        }


        public Task Delete(LoginReport entity)
        {
            return _unitOfWork.LoginRe.Delete(entity);
        }

        public Task<LoginReportReponse> GetALl(LoginReportSerarchRequest request)
        {
            return _unitOfWork.LoginRe.GetALl(request);
        }
        public async Task<dynamic> DataLogin(LoginReportSerarchRequest request)
        {
            var data = _unitOfWork.LoginRe.ExportLogin(request);
            var listData = data.Result;
            return listData;
        }
        public async Task<string> ExportLogin(LoginReportSerarchRequest request)
        {
            var timeHanle = request.From.Value;
            var rootPath = "C:\\local\\workingTime";
            var exists = Directory.Exists(rootPath);
            if (!exists)
                Directory.CreateDirectory(rootPath);
            var pathFile = "working_time_" + timeHanle.ToString("yyyyMMdd") + ".xlsx";
            if (File.Exists(pathFile))
                File.Delete(pathFile);
            var filepath = Path.Combine(rootPath, pathFile);
            using (var document = SpreadsheetDocument.Create(filepath,
                       SpreadsheetDocumentType.Workbook))
            {
                var relationshipId = "actionCodeOverview";
                var workbookPart = document.AddWorkbookPart();
                var workbook = new Workbook();
                var sheets = new Sheets();
                var sheet1 = new Sheet
                {
                    Name = "Sheet1",
                    SheetId = 1,
                    Id = relationshipId
                };
                sheets.Append(sheet1);
                workbook.Append(sheets);
                workbookPart.Workbook = workbook;
                var workSheetPart = workbookPart.AddNewPart<WorksheetPart>(relationshipId);
                var workSheet = new Worksheet();
                var sheetData = new SheetData();
                workSheet.Append(sheetData);
                workSheetPart.Worksheet = workSheet;
                document.PackageProperties.Creator = "Vietstargroup";
                document.PackageProperties.Created = DateTime.UtcNow;
                var data = _unitOfWork.LoginRe.ExportLogin(request);
                var listData = data.Result;

                var rowHeader = new Row();
                rowHeader.RowIndex = 1;
                rowHeader.Append(
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("USERNAME")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("CHECK_IN")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("CHECK_OUT")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("DURATION_IN_HOUR")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("WEEK_DAY")
                    }
                );
                sheetData.Append(rowHeader);
                uint indexloop = 2;
                foreach (var item in listData)
                {
                    var rowAppend = new Row();
                    rowAppend.RowIndex = indexloop;
                    rowAppend.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.UserName)
                        },
                        new Cell
                        {
                            DataType = CellValues.Date,

                            CellValue = item.CheckIn.HasValue == true ? new CellValue(item.CheckIn.Value) : new CellValue(""),
                        },
                        new Cell
                        {
                            DataType = CellValues.Date,
                            CellValue = item.Checkout.HasValue == true ? new CellValue(item.Checkout.Value) : new CellValue(""),
                        },
                        new Cell
                        {
                            DataType = CellValues.Number,
                            CellValue = new CellValue(item.Duration)
                        },
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.GetDayText)
                        }
                    );
                    sheetData.Append(rowAppend);
                    indexloop++;

                }
                document.Save();
            }
            return pathFile;

        }



        public Task<LoginReport> Getbyid(string Id)
        {
            return _unitOfWork.LoginRe.GetById(Id);
        }

        public Task<LoginReport> GetByIdAsync(string id)
        {
            return _unitOfWork.LoginRe.GetById(id);
        }
        public Task<int> UpdateAsyn(LoginReport entity)
        {

            return _unitOfWork.LoginRe.Update(entity);
        }

    }
}
