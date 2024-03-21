using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Model;
using VS.core.Request;
using Workbook = DocumentFormat.OpenXml.Spreadsheet.Workbook;
using Sheets = DocumentFormat.OpenXml.Spreadsheet.Sheets;
using Worksheet = DocumentFormat.OpenXml.Spreadsheet.Worksheet;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace VS.Core.Business;

public class DailyReportBussiness : IDailyReportBussiness
{
    private readonly IUnitOfWork _unitOfWork1;

    public DailyReportBussiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork1 = unitOfWork;
    }

    public Task<bool> HandleExportFile(CampagnProfileExportRequest request, string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<string> ExportFileTotal(CampagnProfileExportRequest request, string userName)
    {
        var dateGet = DateTime.Now;
        var fileName = dateGet.ToString("dd.MM.yy") + ".xlsx";
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\DaillyReport";
        var pathFolder = Path.Combine(rootPath, userName);
        var exists = Directory.Exists(pathFolder);
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile)) File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "rId1";
            //build Workbook Part
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
            //add document properties
            document.PackageProperties.Creator = "nghiait";
            document.PackageProperties.Created = DateTime.UtcNow;
            var requestQuery = new CampagnProfileExportRequest
            {
                CampaignId = request.CampaignId,
                From = request.From,
                To = request.To,
                Limit = 50000,
                Page = 1,
                VendorId = null
            };
            var resultData = await _unitOfWork1.DailyReport.GetAllCase(requestQuery);
            var listData = resultData.Data as List<CampagnProileExportIndexModel>;
            var indexloop = 1;
            var row1 = new Row();
            row1.RowIndex = 1;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("CONTRACT NUMBER")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Assigned code")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Action Code")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Contact method")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Contact info")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Group case")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.Date,
                    CellValue = new CellValue("Create at")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.Date,
                    CellValue = new CellValue("Update at")
                }
            );
            sheetData.Append(row1);

            foreach (var item in listData)
            {
                indexloop++;
                var row = new Row();
                row.RowIndex = (uint)indexloop;
                var cell2 = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.NoAgreement)
                };

                var cell3 = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.UserName)
                };

                row.Append(cell2);
                row.Append(cell3);
                var actionCode = item.Code;
                if (!string.IsNullOrEmpty(actionCode))
                {
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.Code)
                        }
                    );

                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.WayContact)
                        }
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.PlaceCode)
                        }
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.ColorCode)
                        }
                    );
                }
                else
                {
                    var cellEmpty = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Mới")
                    };


                    row.Append(
                        cellEmpty
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );

                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );

                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );
                }

                var itemCreaeAt = item.CreateAt.Value;

                
                var itemUpdate = itemCreaeAt;
                if ( item.UpdateAt == null || item.UpdateAt.HasValue == false)
                {
                    
                }
                else
                {
                    itemUpdate = item.UpdateAt.Value;
                }
                row.Append(
                    new Cell
                    {
                        DataType = CellValues.Date,
                        CellValue = new CellValue(itemCreaeAt)
                    }
                );

                row.Append(
                    new Cell
                    {
                        DataType = CellValues.Date,
                        CellValue = new CellValue(itemUpdate)
                    }
                );  
                sheetData.Append(row);
            }

            document.Save();
        }

        return "";
    }



    public async Task<string> ExportFileExcel(CampagnProfileExportRequest request, string userName)
    {
        var dateGet = DateTime.Now;
        var fileName = dateGet.ToString("dd.MM.yy") + ".xlsx";
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\report";
        var pathFolder = Path.Combine(rootPath, userName);
        var exists = Directory.Exists(pathFolder);
        var pathfileName = userName + "\\" + fileName;
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile)) File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "report";
            //build Workbook Part
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
            //add document properties
            document.PackageProperties.Creator = "nghiait";
            document.PackageProperties.Created = DateTime.UtcNow;
            var requestQuery = new CampagnProfileExportRequest
            {
                CampaignId = request.CampaignId,
                From = request.From,
                To = request.To,
                Limit = 50000,
                Page = 1,
                UserId = request.UserId
            };
            var resultData = await _unitOfWork1.DailyReport.GetAllCasev2(requestQuery);
            var listData = resultData.Data as List<CampagnProileExportIndexModel>;
            var indexloop = 1;
            var row1 = new Row();
            row1.RowIndex = 1;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("CONTRACT NUMBER\r\nHợp đồng")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Customer name\r\nKhách hàng 3")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("BOD 4")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("ID card no\r\nCMND 8")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Date of sign 6")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("PRODUCTCODE\r\nMã sản phẩm")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Product name\r\nTên sản phẩm 7")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("VIN\r\nSố khung")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("MRC Plate\r\nBiển số xe")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Product Price\r\nGiá sản phẩm")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Down payment\r\nSố tiền trả trước")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Amount of credit\r\nTiền vay 9")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Sum of payment\r\nTổng tiền phải trả 14")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Installment\r\nSố kỳ phải trả 10")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Monthly payment\r\nSố tiền trả theo kỳ 11")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("First installment\r\nNgày trả đầu tiên")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Last installment\r\nNgày trả cuối cùng")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Count of payment\r\nSố kỳ đã trả")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Sum of penalty\r\nTổng tiền phạt")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Sum of penalty paid\r\nTiền phạt đã trả")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("The rest of penalty\r\nTiền phạt còn lại")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Date of last payment")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Total paid\r\nSố tiền đã trả")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Total debt\r\nSố nợ hiện tại 21")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Current DPD 15")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("MOBIPHONE\r\nSDT KH 16")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("HOMEPHONE\r\nSDT nhà")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("REL2_PHONE\r\nSDT người thân 1")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("REL2_PHONE\r\nSDT người thân 2")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("WORKING_PHONE\r\nSDT nơi làm việc")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Permanent address\r\nĐ/c thường trú 18")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Permanent district\r\nQuận thường trú 19")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Permanent Region\r\nKhu vực thường trú 20")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Contact address\r\nĐ/c liên lạc")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Contact district\r\nQuận liên lạc")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Region\r\nKhu vực")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Name of employer\r\nNghề nghiệp 9")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("GHI CHÚ BAN ĐẦU")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Tham Chiếu Rel info 17")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("   id 39")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("  Tình trạng")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Phương thức liên hệ\r\n")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Nơi liên hệ")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Phân loại hồ sơ")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày tạo")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày cập nhật")
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
                    CellValue = new CellValue(item.NoAgreement)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.CustomerName)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Date,
                    CellValue = new CellValue(item.DayOfBirth)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.NationalId)
                });

              

                var valueDateText = "";
                if (item.RegisterDay.HasValue)
                {
                    valueDateText = item.RegisterDay.Value.ToString("dd/MM/yyyy");
                }
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(valueDateText)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.CodeProduct)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.NameProduct)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.AmountLoan)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.TotalMoneyPaid)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.Tenure)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.EMI)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });

                var lastPaidDayText = "";
                if (item.LastPadDay.HasValue)
                {
                    lastPaidDayText = item.LastPadDay.Value.ToString("dd/MM/yyyy");
                }
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(lastPaidDayText)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.NoTenure)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.TotalFines)
                });

                

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.TotalPaid)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.DebitOriginal)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.Number,
                    CellValue = new CellValue(item.DPD.HasValue?item.DPD.Value:0)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.MobilePhone)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.HouseNumber)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Phone1)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.OtherPhone)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.OfficeNumber)
                });


                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Road)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.SuburbanDir)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Provice)
                });



                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Road1)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.SuburbanDir1)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Provice1)
                });
                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.NoteFirstTime)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.NoteRel)
                });

                row.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.UserName)
                });
                var actionCode = item.Code;
                if (!string.IsNullOrEmpty(actionCode))
                {
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.CodeInput)
                        }
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.WayContact)
                        }
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.PlaceCode)
                        }
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(item.ColorCode)
                        }
                    );
                }
                else
                {
                    var cellEmpty = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue("Mới")
                    };


                    row.Append(
                        cellEmpty
                    );
                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );

                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );

                    row.Append(
                        new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        }
                    );
                }

                var itemCreaeAt = item.CreateAt.Value;
                var itemUpdate = itemCreaeAt;
                if (item.UpdateAt == null || item.UpdateAt.HasValue == false)
                {
                }
                else
                {
                    itemUpdate = item.UpdateAt.Value;
                }
                row.Append(
                    new Cell
                    {
                        DataType = CellValues.Date,
                        CellValue = new CellValue(itemCreaeAt)
                    }
                );

                row.Append(
                    new Cell
                    {
                        DataType = CellValues.Date,
                        CellValue = new CellValue(itemUpdate)
                    }
                );
                sheetData.Append(row);
            }

            document.Save();
        }

        return fileName;

    }
}