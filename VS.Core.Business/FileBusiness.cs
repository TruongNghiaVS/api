using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using VS.core.Request;
using VS.Core.Business.GlobalClass;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{



    public class FileBusiness : BaseBusiness, IFileBussiness
    {

        public static List<CampagnProfile> ListCall { get; set; }
        private static List<string> ChanelCall { get; set; }
        private DataCallContainer DataCall { get; set; }

        private List<Object> listData = new List<Object>();
        private ICallLogBussiness CallLogBussiness;
        public FileBusiness(IUnitOfWork unitOfWork,
            ICallLogBussiness callLogBussiness) : base(unitOfWork)
        {
            ListCall = new List<CampagnProfile>();
            ChanelCall = new List<string>();
            DataCall = DataCallContainer.GlobalContainer();
            CallLogBussiness = callLogBussiness;
        }



        public async Task<string> GetFileResult()
        {
            var dateGet = DateTime.Now;
            var fileName = dateGet.ToString("dd.MM.yy") + ".xlsx";
            var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\report";
            var pathFolder = Path.Combine(rootPath, "ResultCall");
            var exists = Directory.Exists(pathFolder);
            var pathfileName = "ResultCall" + "\\" + fileName;
            if (!exists)
                Directory.CreateDirectory(pathFolder);
            var pathFile = Path.Combine(pathFolder, fileName);
            if (File.Exists(pathFile)) File.Delete(pathFile);

            using (var document = SpreadsheetDocument.Create(pathFile,
                       SpreadsheetDocumentType.Workbook))
            {
                var relationshipId = "resultCall";
                var workbookPart = document.AddWorkbookPart();
                var workbook = new Workbook();
                var sheets = new Sheets();
                var sheet1 = new Sheet
                {
                    Name = "report",
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

                document.PackageProperties.Creator = "nghiait";
                document.PackageProperties.Created = DateTime.UtcNow;

                var resultData = await _unitOfWork.ReportTalkTimeRepository.GetALl(new ReportTalkTimeRequest());
                var listData = resultData.Data as List<ReportTalkTimeIndexModel>;
                var indexloop = 1;
                var row1 = new Row();
                row1.RowIndex = 1;
                row1.Append(
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Số hợp đồng")
                    }
                );

                row1.Append(
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Số điện thoại")
                    }
                );
                row1.Append(
                  new Cell
                  {
                      DataType = CellValues.String,
                      CellValue = new CellValue("gọi bởi")
                  }
              );
                row1.Append(
                  new Cell
                  {
                      DataType = CellValues.String,
                      CellValue = new CellValue("Trạng thái")
                  }
              );


                sheetData.Append(row1);

                foreach (var item in listData)
                {
                    indexloop++;
                    var row = new Row();
                    row.RowIndex = (uint)indexloop;
                    row.Append(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.NoAgree)
                    });

                    row.Append(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.PhoneLog)
                    });
                    row.Append(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.LineCode)
                    });


                    row.Append(new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.StatusCall)
                    });

                    sheetData.Append(row);
                }

                document.Save();
            }

            return fileName;

        }
    }


}
