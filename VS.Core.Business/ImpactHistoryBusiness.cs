using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.Campagn;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;

namespace VS.Core.Business
{
    public class ImpactHistoryBusiness : BaseBusiness, IImpactHistoryBussiness
    {

        public ImpactHistoryBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> Add(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.Add(entity);
        }


        public Task Delete(ImpactHistory entity)
        {
            return _unitOfWork.ImpactRe.Delete(entity);
        }

        public Task<ImpactHistoryReponse> GetALl(ImpactHistorySerarchRequest request)
        {
            return _unitOfWork.ImpactRe.GetALl(request);
        }

        public Task<ImpactHistoryReponse> GetFinal(ImpactHistorySerarchRequest request)
        {
            return _unitOfWork.ImpactRe.GetFinal(request);
        }


        public async Task<string> ExportFileCallReportMirae(ImpactHistoryExportMiraeCallReqeust request)
        {

            return await ExportDataCallReportForMirae(request);
        }


        public async Task<dynamic> GetDataReportMirae(ImpactHistoryExportMiraeCallReqeust request = null)
        {
            var dateGet = request.From;
            var data = await _unitOfWork.ImpactRe.ExportMiraeCallReport(dateGet, dateGet);
            var listData = data.Data;
            return listData;
        }

        public async Task<string> ExportDataCallReportForMirae(ImpactHistoryExportMiraeCallReqeust request)
        {
            var dateGet = request.From;
            var rootPath = "C:\\local\\callReport";
            var exists = Directory.Exists(rootPath);
            if (!exists)
                Directory.CreateDirectory(rootPath);
            var pathFile = " call_report_" + dateGet.ToString("yyyyMMdd") + ".xlsx";
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
                var data = await _unitOfWork.ImpactRe.ExportMiraeCallReport(dateGet, dateGet);
                var listData = data.Data;
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
                        CellValue = new CellValue("TEAM_LEAD")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("AGREEMENT_ID")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("ACTION_CODE")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("PROMISE_DATE")
                    },
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("PROMISE_AMT")
                    },
                     new Cell
                     {
                         DataType = CellValues.String,
                         CellValue = new CellValue("REMARK")
                     }
                     ,
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("CALL_DATE")
                    },
                     new Cell
                     {
                         DataType = CellValues.String,
                         CellValue = new CellValue("CONTACT_PERSON")
                     }
                );
                sheetData.Append(rowHeader);
                uint indexloop = 2;
                foreach (var itemdraw in listData)
                {

                    var item = itemdraw as MiraeCallReportIndexModel;
                    var rowAppend = new Row();
                    rowAppend.RowIndex = indexloop;
                    rowAppend.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.Username)
                        },
                         new Cell
                         {
                             DataType = CellValues.String,
                             CellValue = new CellValue(item.Teamlead)
                         },
                          new Cell
                          {
                              DataType = CellValues.String,
                              CellValue = new CellValue(item.Agrreement_id)
                          },
                           new Cell
                           {
                               DataType = CellValues.String,
                               CellValue = new CellValue(item.Action_code)
                           },

                        new Cell
                        {
                            DataType = CellValues.Date,
                            CellValue = item.Primise_date.HasValue ? new CellValue(item.Primise_date.Value) : new CellValue()

                        },
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.Promise_amt),

                        },
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.Remark)
                        },
                        new Cell
                        {
                            DataType = CellValues.Date,
                            CellValue = item.Calldate.HasValue ? new CellValue(item.Calldate.Value) : new CellValue()
                        },
                          new Cell
                          {
                              DataType = CellValues.String,
                              CellValue = new CellValue(item.ContactPerson)
                          }
                    );
                    sheetData.Append(rowAppend);
                    indexloop++;

                }
                document.Save();
            }
            return pathFile;

        }


        public Task<ImpactHistory> Getbyid(string Id)
        {
            return _unitOfWork.ImpactRe.GetById(Id);
        }

        public Task<ImpactHistory> GetByIdAsync(string id)
        {
            return _unitOfWork.ImpactRe.GetById(id);
        }

        public Task<int> UpdateAsyn(ImpactHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
