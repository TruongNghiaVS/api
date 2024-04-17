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
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Linq;
using Org.BouncyCastle.Asn1.Crmf;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Drawing;
using System.IO;

namespace VS.Core.Business;

public class ReportCrmBussiness : IReportCrmBussiness
{
    private readonly IUnitOfWork _unitOfWork1;

    public ReportCrmBussiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork1 = unitOfWork;
    }

    private string ConvertToHHMMSS(string number1)
    {
        if( string.IsNullOrEmpty(number1))
        {
            return "00:00:00";
        }


        double? number = 0;

        try
        {
            number =  double.Parse(number1);
        }
        catch (Exception)
        {

            return "";
        }
        if (!number.HasValue || number == 0)
        {
            return "00:00:00";
        }

        double numberRe = (double)number;
        var timeSpan = TimeSpan.FromSeconds(numberRe);
        int hh = timeSpan.Hours;
        int mm = timeSpan.Minutes;
        int ss = timeSpan.Seconds;
        return hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
    }
    private string ConvertToHHMMSS(double number2)
    {
        if( number2 < 1)
        {
            return "00:00:00";
        }
        var number1 = number2.ToString();

        if (string.IsNullOrEmpty(number1))
        {
            return "00:00:00";
        }

        double? number = double.Parse(number1);
        if (!number.HasValue || number == 0)
        {
            return "00:00:00";
        }

        double numberRe = (double)number;
        var timeSpan = TimeSpan.FromSeconds(numberRe);
        int hh = timeSpan.Hours;
        int mm = timeSpan.Minutes;
        int ss = timeSpan.Seconds;
        return hh.ToString("00") + ":" + mm.ToString("00") + ":" + ss.ToString("00");
    }

    public async Task<string> ExportFileTaltime(
         CrmReportRequest request
        )
    {

        Random rnd = new Random();
   
        var dateGet = DateTime.Now;
        var fileName = request.UserName+"_ReportCall."+ dateGet.ToString("dd.MM.yy") + rnd.Next(10, 100) + ".xlsx";      
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\crmReport\\reportCall";
        var pathFolder = System.IO.Path.Combine(rootPath, request.UserName);
        var exists = Directory.Exists(pathFolder);
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = System.IO.Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile)) 
            File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "reportCall";
            var workbookPart = document.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();
            var sheet1 = new Sheet
            {
                Name = "reportCall",
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
            document.PackageProperties.Creator = "VietstarGroup";
            document.PackageProperties.Created = DateTime.UtcNow;
            var requestQuery = new CrmReportRequest
            {
                
                From = request.From,
                To = request.To,
                UserId = request.UserId
               
            };
            var resultData = await _unitOfWork1.DailyReport.ExprotCrmTalkTime(requestQuery);
            var listData = resultData.Data as List<CrmReprotIndexModel>;


            var listOUtput1 = listData.GroupBy(u => u.ManagerFullName)
            .Select(grp => grp.ToList())
            .ToList();
            var indexloop = 1;
            var rowHeader = new Row();
            rowHeader.RowIndex = 1;
            rowHeader.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày Báo cáo")
                }
            );
            rowHeader.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue(DateTime.Now.ToString("dd/MM/yyyy"))
               }
            );
            sheetData.Append(rowHeader);

            var rowempty = new Row();
            rowempty.RowIndex = 2;
             sheetData.Append(rowempty);
            indexloop++;
            var row1 = new Row();
            row1.RowIndex = 2;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Họ và tên")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Số HĐ")
                }
            );
      

            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Tổng cuộc gọi")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Talk gọi")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Talk chờ")
                }
            );

            row1.Append(
                new Cell
                {
                    DataType = CellValues.Date,
                    CellValue = new CellValue("Talk đàm thoại")
                }
            );
          
            sheetData.Append(row1);
            indexloop++;

            foreach (var itemData in listOUtput1)
            {
                var rowinfoLead = new Row();
                var itemfrist = itemData.First();
                rowinfoLead.RowIndex = (uint)indexloop;
                if (itemData.Count < 2)
                {
                    var cell22 = new Cell
                    {
                        DataType = CellValues.String,

                        CellValue = new CellValue(itemfrist.ManagerFullName)

                    };
                    rowinfoLead.Append(cell22);

                }
                else
                {
                    var cell22 = new Cell
                    {
                        DataType = CellValues.String,

                        CellValue = new CellValue(itemfrist.ManagerFullName + " total")
                    };
                    rowinfoLead.Append(cell22);
                } 
                    



                var sohd = itemData.Sum(x => int.Parse(x.SumNoAgree));
                var sumCall = itemData.Sum(x => int.Parse(x.SumCall));
                var talkCall = itemData.Sum(x => x.Timcall);
                var timewaiting = itemData.Sum(x => x.TimeWaiting);
                var timeTalking = itemData.Sum(x => x.TimeTalking);


             
                rowinfoLead.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(sohd)
                });

                rowinfoLead.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(sumCall)
                });

                rowinfoLead.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(ConvertToHHMMSS(talkCall))
                });
                rowinfoLead.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(ConvertToHHMMSS( timewaiting))
                });
                rowinfoLead.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(ConvertToHHMMSS(timeTalking))
                });
                sheetData.Append(rowinfoLead);

             
                foreach (var item in itemData)
                {

                    indexloop++;
                    var row = new Row();
                    row.RowIndex = (uint)indexloop;
                    var cell2 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.FullName)
                    };

                    var cell3 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.SumNoAgree)
                    };

                    var cell4 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.SumCall)
                    };

                    var cell5 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(ConvertToHHMMSS(item.Timcall))
                    };
                    var cell6 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(ConvertToHHMMSS(item.TimeWaiting))
                    };
                    var cell7 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(ConvertToHHMMSS(item.TimeTalking))
                    };

                    row.Append(cell2);
                    row.Append(cell3);
                    row.Append(cell4);
                    row.Append(cell5);
                    row.Append(cell6);
                    row.Append(cell7);
                    sheetData.Append(row);
                }
                indexloop++;
            }
            indexloop++;

            var rowinfofooter = new Row();

            rowinfofooter.RowIndex = (uint)indexloop;

            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue("Grand total")
            });


            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(listData.Sum(x=> int.Parse(x.SumNoAgree)))
            });

            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(listData.Sum(x => int.Parse(x.SumCall)))
            });

            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue( ConvertToHHMMSS( listData.Sum(x => x.Timcall)))
            });

            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(ConvertToHHMMSS(listData.Sum(x => x.TimeWaiting)))
            });
            rowinfofooter.Append(new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(ConvertToHHMMSS(listData.Sum(x => x.TimeTalking)))
            });
            sheetData.Append(rowinfofooter);
            document.Save();
            
        }

        return pathFile;
    }


    public async Task<string> ExportStatusOverview(
     CrmReportRequest request
    )
    {

       

        Random rnd = new Random();
        var dateGet = DateTime.Now;
        var fileName = request.UserName + "_tacdong." + dateGet.ToString("dd.MM.yy") +rnd.Next(10,100) + ".xlsx";
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\groupStatus";
        var pathFolder = System.IO.Path.Combine(rootPath, request.UserName);
        var exists = Directory.Exists(pathFolder);
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = System.IO.Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile))

            File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "actionCodeOverview";
            var relationshipI2 = "detailactionCode";
            var workbookPart = document.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();
            var sheet1 = new Sheet
            {
                Name = "Báo cáo tác động(tổng quan)",
                SheetId = 1,
                Id = relationshipId
            };
            var sheet2 = new Sheet
            {
                Name = "Báo cáo chi tiết (Tác động)",
                SheetId = 2,
                Id = relationshipI2
            };


            sheets.Append(sheet1);
            sheets.Append(sheet2);
            workbook.Append(sheets);
            workbookPart.Workbook = workbook;
            var workSheetPart = workbookPart.AddNewPart<WorksheetPart>(relationshipId);
            var workSheetPart2 = workbookPart.AddNewPart<WorksheetPart>(relationshipI2);

            var workSheet = new Worksheet();
            var sheetData = new SheetData();

            workSheet.Append(sheetData);
            workSheetPart.Worksheet = workSheet;

            var workSheet2 = new Worksheet();
            var sheetData2 = new SheetData();

            workSheet2.Append(sheetData2);
            workSheetPart2.Worksheet = workSheet2;

            document.PackageProperties.Creator = "Vietstargroup";
            document.PackageProperties.Created = DateTime.UtcNow;
            var requestQuery = new CrmReportRequest
            {

                From = request.From,
                To = request.To,
                UserId = request.UserId

            };
            var resultData = await _unitOfWork1.DailyReport.ExprotCrmByStatus(requestQuery);
            var listData = resultData.Data as List<CrmReprotStatusIndexModel>;
            var indexloop = 1;
            var rowHeader = new Row();
            rowHeader.RowIndex = 1;
            rowHeader.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày Báo cáo")
                }
            );

           rowHeader.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue(DateTime.Now.ToString("dd/MM/yyyy"))
               }
           );

            sheetData.Append(rowHeader);

            var rowempty = new Row();
            rowempty.RowIndex = 2;
            sheetData.Append(rowempty);
            indexloop++;
            var row1 = new Row();
            row1.RowIndex = 3;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Team lead")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Nhân viên")
                }
            );


            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Total")
                }
            );

            sheetData.Append(row1);
            indexloop++;

            var managerNameWrite1 = "";


            var listOUtput1 = listData.GroupBy(u => u.ManagerFullName)
         .Select(grp => grp.ToList())
         .ToList();

            foreach (var itemlead in listOUtput1)
            {

                var totalgrand = itemlead.Sum(x => int.Parse(x.Total));

                var lastItem = itemlead.Last();
                foreach (var item in itemlead)
                {
                    indexloop++;

                    var row = new Row();
                    row.RowIndex = (uint)indexloop;



                    if (item.ManagerFullName != managerNameWrite1)
                    {
                        managerNameWrite1 = item.ManagerFullName;

                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(managerNameWrite1)
                        };
                        row.Append(cell2);
                    }
                    else
                    {
                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        };
                        row.Append(cell2);

                    }

                    var cell3 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.FullName)
                    };

                    var cell4 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.Total)
                    };



                    row.Append(cell3);
                    row.Append(cell4);

                    sheetData.Append(row);

                   

                }
                indexloop++;
                var row2 = new Row(); 
                row2.RowIndex = (uint)indexloop;
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(lastItem.ManagerFullName + " total")
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(totalgrand)
                });
                sheetData.Append(row2);
             

            }

           

          
            var grandTotalStatus = listData.Sum(x => int.Parse(x.Total));

            var rowFooter = new Row();
            indexloop++;
            rowFooter.RowIndex = (uint)indexloop;

            rowFooter.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue("Grand total")
               }
           );

            rowFooter.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue("")
               }
           );
            rowFooter.Append(
            new Cell
            {
            DataType = CellValues.String,
            CellValue = new CellValue(grandTotalStatus)
            }
            );
            sheetData.Append(rowFooter);

            var indexloop2 = 1;
            var rowHeader2 = new Row();
            rowHeader2.RowIndex = 1;
            rowHeader2.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày Báo cáo2")
                }
            );

            rowHeader2.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue(DateTime.Now.ToString("dd/MM/yyyy"))
               }
           );


            sheetData2.Append(rowHeader2);
            var resultData2 = await _unitOfWork1.DailyReport.ExprotCrmByStatusDetail(requestQuery);
            var listData2 = resultData2.Data as List<CrmReprotStatusIndexModel>;
            
            var listOUtput = listData2.GroupBy(u => u.CreatedBy)
            .Select(grp => grp.ToList())
            .ToList();
            var dataList = new List<CrmReportStatusDeatailIndexModel>();
            foreach (var createby in listOUtput)
            {
                var itemData = new CrmReportStatusDeatailIndexModel();
                var itemfrist = createby.FirstOrDefault();
                var sumTotal = createby.Sum(x => x.Total2);
                if(itemfrist == null)
                {
                    continue;
                }
                itemData.ManagerFullName = itemfrist.ManagerFullName;
                itemData.FullName = itemfrist.FullName;
                itemData.TotalGrand = sumTotal;

                foreach (var item in createby)
                {
                  
                    if( item.ReasonCode == "BPTP")
                    {
                        itemData.BPTP = item.Total2;
                    }

                    if (item.ReasonCode == "CALLBACK")
                    {
                        itemData.CALLBACK = item.Total2;
                    }
                    if (item.ReasonCode == "CONFIRM")
                    {
                        itemData.CONFIRM = item.Total2;
                    }

                    if (item.ReasonCode == "DIE")
                    {
                        itemData.DIE = item.Total2;
                    }
                    if (item.ReasonCode == "DIF")
                    {
                        itemData.DIF = item.Total2;
                    }

                    if (item.ReasonCode == "LM")
                    {
                        itemData.LM = item.Total2;
                    }
                    if (item.ReasonCode == "LMN")
                    {
                        itemData.LMN = item.Total2;
                    }

                    if (item.ReasonCode == "NCON")
                    {
                        itemData.NCON = item.Total2;
                    }
                    if (item.ReasonCode == "OTHER")
                    {
                        itemData.OTHER = item.Total2;
                    }

                    if (item.ReasonCode == "PTP")
                    {
                        itemData.PTP = item.Total2;
                    }
                    if (item.ReasonCode == "RFTP")
                    {
                        itemData.RFTP = item.Total2;
                    }
                    if (item.ReasonCode == "RONA")
                    {
                        itemData.RONA = item.Total2;
                    }
                }
                
                


                
                dataList.Add(itemData);

                //var itemfooter = new CrmReportStatusDeatailIndexModel();


                //itemData.ManagerFullName = itemData.ManagerFullName+" Total";
                //itemData.FullName = "";
                //itemData.BPTP = createby.Where(x => x.ReasonCode == "BPTP").Sum(x => x.Total2);
                //itemData.CALLBACK = createby.Where(x => x.ReasonCode == "CALLBACK").Sum(x => x.Total2);
                //itemData.CONFIRM = createby.Where(x => x.ReasonCode == "CONFIRM").Sum(x => x.Total2);
                //itemData.DIE = createby.Where(x => x.ReasonCode == "DIE").Sum(x => x.Total2);
                //itemData.DIF = createby.Where(x => x.ReasonCode == "DIF").Sum(x => x.Total2);
                //itemData.LM = createby.Where(x => x.ReasonCode == "LM").Sum(x => x.Total2);
                //itemData.LMN = createby.Where(x => x.ReasonCode == "LMN").Sum(x => x.Total2);
                //itemData.NCON = createby.Where(x => x.ReasonCode == "NCON").Sum(x => x.Total2);
                //itemData.OTHER = createby.Where(x => x.ReasonCode == "OTHER").Sum(x => x.Total2);
                //itemData.PTP = createby.Where(x => x.ReasonCode == "PTP").Sum(x => x.Total2);
                //itemData.RFTP = createby.Where(x => x.ReasonCode == "RFTP").Sum(x => x.Total2);
                //itemData.RONA = createby.Where(x => x.ReasonCode == "RONA").Sum(x => x.Total2);

                //itemData.TotalGrand = createby.Where(x => x.ReasonCode == "RONA").Sum(x => x.Total2);
            }

           
            var rowHeader3 = new Row();
            rowHeader3.RowIndex = 2;
            rowHeader3.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Team lead")
                }
            );

            rowHeader3.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue("Nhân viên")
               }
           );

            rowHeader3.Append(
             new Cell
             {
                 DataType = CellValues.String,
                 CellValue = new CellValue("BPTP")
             }
         );

          rowHeader3.Append(
          new Cell
          {
              DataType = CellValues.String,
              CellValue = new CellValue("CALLBACK")
          }
      );

            rowHeader3.Append(
          new Cell
          {
              DataType = CellValues.String,
              CellValue = new CellValue("CONFIRM")
          }
      );

            rowHeader3.Append(
     new Cell
     {
         DataType = CellValues.String,
         CellValue = new CellValue("DIE")
     }
 );

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("DIF")
}
);

            rowHeader3.Append(
new Cell
{
 DataType = CellValues.String,
 CellValue = new CellValue("LM")
}
);
            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("LMN")
}
);

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("NCON")
}
);

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("OTHER")
}
);
            rowHeader3.Append(
new Cell
{
    DataType = CellValues.String,
    CellValue = new CellValue("PTP")
}
);

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("RFTP")
}
);

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("RONA")
}
);

            rowHeader3.Append(
new Cell
{
DataType = CellValues.String,
CellValue = new CellValue("Grand Total")
}
);
            sheetData2.Append(rowHeader3);
            indexloop2++;
            var managerNameWrite = "";




            var listOUtput2 = dataList.GroupBy(u => u.ManagerFullName)
     .Select(grp => grp.ToList())
     .ToList();

            foreach (var itemdata in listOUtput2)
            {

                foreach (var item in itemdata)
                {


                    indexloop2++;
                    var row = new Row();
                    row.RowIndex = (uint)indexloop2;
                    if (item.ManagerFullName != managerNameWrite)
                    {
                        managerNameWrite = item.ManagerFullName;

                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(managerNameWrite)
                        };
                        row.Append(cell2);
                    }
                    else
                    {
                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        };
                        row.Append(cell2);

                    }



                    var cell3 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.FullName)
                    };

                    var cell4 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.BPTP)
                    };
                    var cell5 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.CALLBACK)
                    };

                    var cell6 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.CONFIRM)
                    };
                    var cell7 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.DIE)
                    };

                    var cell8 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.DIF)
                    };

                    var cell9 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.LM)
                    };
                    var cell10 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.LMN)
                    };
                    var cell11 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.NCON)
                    };
                    var cell12 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.OTHER)
                    };
                    var cell13 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.PTP)
                    };
                    var cell14 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.RFTP)
                    };
                    var cell15 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.RONA)
                    };
                    var cell16 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.TotalGrand)
                    };

                    row.Append(cell3);
                    row.Append(cell4);
                    row.Append(cell5);
                    row.Append(cell6);
                    row.Append(cell7);
                    row.Append(cell8);
                    row.Append(cell9);
                    row.Append(cell10);
                    row.Append(cell11);
                    row.Append(cell12);
                    row.Append(cell13);
                    row.Append(cell14);
                    row.Append(cell15);
                    row.Append(cell16);

                    sheetData2.Append(row);

                }

                var lastItem = itemdata.Last();

                var itemfooter = new CrmReportStatusDeatailIndexModel();


                itemfooter.BPTP = itemdata.Sum(x => x.BPTP);
                itemfooter.CALLBACK = itemdata.Sum(x => x.CALLBACK);
                itemfooter.CONFIRM = itemdata.Sum(x => x.CONFIRM);
                itemfooter.DIE = itemdata.Sum(x => x.DIE);
                itemfooter.DIF = itemdata.Sum(x => x.DIF);
                itemfooter.LM = itemdata.Sum(x => x.LM);
                itemfooter.LMN = itemdata.Sum(x => x.LMN);
                itemfooter.NCON = itemdata.Sum(x => x.NCON);
                itemfooter.OTHER = itemdata.Sum(x => x.OTHER);
                itemfooter.PTP = itemdata.Sum(x => x.PTP);
                itemfooter.RFTP = itemdata.Sum(x => x.RFTP);
                itemfooter.RONA = itemdata.Sum(x => x.RONA);

                itemfooter.TotalGrand = itemfooter.BPTP + itemfooter.CALLBACK + itemfooter.CONFIRM
                    + itemfooter.DIF + itemfooter.DIE + itemfooter.LM + itemfooter.LMN + itemfooter.NCON
                    + itemfooter.OTHER + itemfooter.PTP + itemfooter.RFTP + itemfooter.RONA;

                indexloop2++;
                var row2 = new Row();
                row2.RowIndex = (uint)indexloop2;
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(lastItem.ManagerFullName + " total")
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("")
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.BPTP)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.CALLBACK)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.CONFIRM)
                });

                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.DIE)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.DIF)
                });

                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.LM)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.LMN)
                });

                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.NCON)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.OTHER)
                });

                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.PTP)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.RFTP)
                });
                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.RONA)
                });

                row2.Append(new Cell()
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(itemfooter.TotalGrand)
                });
                sheetData2.Append(row2);

            }

            indexloop2++;
            var row3 = new Row();
            row3.RowIndex = (uint)indexloop2;
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue("Grand total")
            }); 
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue("")
            });

            


            var itemfooter2 = new CrmReportStatusDeatailIndexModel();


            itemfooter2.BPTP = dataList.Sum(x => x.BPTP);
            itemfooter2.CALLBACK = dataList.Sum(x => x.CALLBACK);
            itemfooter2.CONFIRM = dataList.Sum(x => x.CONFIRM);
            itemfooter2.DIE = dataList.Sum(x => x.DIE);
            itemfooter2.DIF = dataList.Sum(x => x.DIF);
            itemfooter2.LM = dataList.Sum(x => x.LM);
            itemfooter2.LMN = dataList.Sum(x => x.LMN);
            itemfooter2.NCON = dataList.Sum(x => x.NCON);
            itemfooter2.OTHER = dataList.Sum(x => x.OTHER);
            itemfooter2.PTP = dataList.Sum(x => x.PTP);
            itemfooter2.RFTP = dataList.Sum(x => x.RFTP);
            itemfooter2.RONA = dataList.Sum(x => x.RONA);

            itemfooter2.TotalGrand = itemfooter2.BPTP + itemfooter2.CALLBACK + itemfooter2.CONFIRM
                + itemfooter2.DIF + itemfooter2.DIE + itemfooter2.LM + itemfooter2.LMN + itemfooter2.NCON
                + itemfooter2.OTHER + itemfooter2.PTP + itemfooter2.RFTP + itemfooter2.RONA;

       
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.BPTP)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.CALLBACK)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.CONFIRM)
            });

            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.DIE)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.DIF)
            });

            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.LM)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.LMN)
            });

            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.NCON)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.OTHER)
            });

            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.PTP)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.RFTP)
            });
            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.RONA)
            });

            row3.Append(new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(itemfooter2.TotalGrand)
            });
            sheetData2.Append(row3);
            document.Save();
        }
        return pathFile;
    }

    public async Task<string> ExportImpactStatusDetail(
     CrmReportRequest request
    )
    {

        Random rnd = new Random();
        var dateGet = DateTime.Now;
        var fileName = dateGet.ToString("dd.MM.yy") + rnd.Next(10, 100) + ".xlsx";
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\crmReport";
        var pathFolder = System.IO.Path.Combine(rootPath, request.UserId);
        var exists = Directory.Exists(pathFolder);
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = System.IO.Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile))

            File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "reportTalktime";
            var workbookPart = document.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();
            var sheet1 = new Sheet
            {
                Name = "reportTalktime",
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
            var requestQuery = new CrmReportRequest
            {

                From = request.From,
                To = request.To,
                UserId = request.UserId

            };
            var resultData = await _unitOfWork1.DailyReport.ExprotCrmByStatusDetail(requestQuery);
            var listData = resultData.Data as List<CrmReprotStatusIndexModel>;
            var indexloop = 1;
            var rowHeader = new Row();
            rowHeader.RowIndex = 1;
            rowHeader.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày Báo cáo")
                }
            );

            rowHeader.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue(DateTime.Now.ToString("dd/MM/yyyy"))
               }
           );

            sheetData.Append(rowHeader);

            var rowempty = new Row();
            rowempty.RowIndex = 2;
            sheetData.Append(rowempty);
            indexloop++;
            var row1 = new Row();
            row1.RowIndex = 3;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Team lead")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Nhân viên")
                }
            );


            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Total")
                }
            );

            sheetData.Append(row1);
            indexloop++;

            foreach (var item in listData)
            {
                indexloop++;
                var row = new Row();
                row.RowIndex = (uint)indexloop;
                var cell2 = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.ManagerName)
                };

                var cell3 = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.FullName)
                };

                var cell4 = new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(item.Total)
                };


                row.Append(cell2);
                row.Append(cell3);
                row.Append(cell4);

                sheetData.Append(row);
            }
            document.Save();
        }

        return pathFile;
    }
    public async Task<string> GetsumoffTalktime(
    CrmReportRequest request
   )
    {
        var dateGet = DateTime.Now;

        Random rnd = new Random();
       


        var fileName = request.UserName + "_sumoffTalkTime." + dateGet.ToString("dd.MM.yy") + rnd.Next(10, 100) + ".xlsx";
        var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\crmReport\\sumupTalktime";
        var pathFolder = System.IO.Path.Combine(rootPath, request.UserName);


        var exists = Directory.Exists(pathFolder);
        if (!exists)
            Directory.CreateDirectory(pathFolder);
        var pathFile = System.IO.Path.Combine(pathFolder, fileName);
        if (File.Exists(pathFile))

            File.Delete(pathFile);
        using (var document = SpreadsheetDocument.Create(pathFile,
                   SpreadsheetDocumentType.Workbook))
        {
            var relationshipId = "sumupTalktime";
            var workbookPart = document.AddWorkbookPart();
            var workbook = new Workbook();
            var sheets = new Sheets();
            var sheet1 = new Sheet
            {
                Name = "sum off phần  trăm kết nối",
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
            var requestQuery = new CrmReportRequest
            {

                From = request.From,
                To = request.To,
                UserId = request.UserId

            };
            var resultData = await _unitOfWork1.DailyReport.GetSumupTalktime(requestQuery);
            var listData = resultData.Data as List<CrmReportTalktimeIndexModel>;

              var listOUtput2 = listData.GroupBy(u => u.ManagerName)
     .Select(grp => grp.ToList())
     .ToList();

            var indexloop = 1;
            var rowHeader = new Row();
            rowHeader.RowIndex = 1;
            rowHeader.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Ngày Báo cáo")
                }
            );

            rowHeader.Append(
               new Cell
               {
                   DataType = CellValues.String,
                   CellValue = new CellValue(DateTime.Now.ToString("dd/MM/yyyy"))
               }
           );

            sheetData.Append(rowHeader);

            var rowempty = new Row();
            rowempty.RowIndex = 2;
            sheetData.Append(rowempty);
            indexloop++;
            var row1 = new Row();
            row1.RowIndex = 3;
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Team lead")
                }
            );
            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Họ tên")
                }
            );


            row1.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue("Total")
                }
            );

            sheetData.Append(row1);
    
            indexloop++;
            var managerNameWrite = "";
            foreach (var itemdata in listOUtput2)
            {

                foreach (var item in itemdata)
                {
                    indexloop++;

                


                    var rowData = new Row();
                    rowData.RowIndex = (uint)indexloop;
                  
                    if (item.ManagerName != managerNameWrite)
                    {
                        managerNameWrite = item.ManagerName;

                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            StyleIndex = (UInt32Value)1U,
                            CellValue = new CellValue(managerNameWrite)
                        };
                        rowData.Append(cell2);
                    }
                    else
                    {
                        var cell2 = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue("")
                        };
                        rowData.Append(cell2);

                    }

                    var cell3 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.FullName)
                    };

                    var cell4 = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(item.Total)
                    };


                   
                    rowData.Append(cell3);
                    rowData.Append(cell4);

                    sheetData.Append(rowData);
                }

                var rowtotal = new Row();
                var firstName = itemdata.First();
                rowtotal.RowIndex = (uint)indexloop;
                rowtotal.Append(
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(firstName.ManagerName +" total"),
                        StyleIndex = (UInt32Value)1U,
                    }
                );
                rowtotal.Append(
                 new Cell
                 {
                     DataType = CellValues.String,
                     CellValue = new CellValue("")
                 }
               );

                rowtotal.Append(
                    new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue(itemdata.Sum(x=>x.Total))
                    }
                );
              
                sheetData.Append(rowtotal);
            }

             indexloop++;



            var rowfooter = new Row();

            rowfooter.RowIndex = (uint)indexloop;
            rowfooter.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    StyleIndex = (UInt32Value)1U,
                    
                    CellValue = new CellValue("Grand total")
                }
            );
            rowfooter.Append(
             new Cell
             {
                 DataType = CellValues.String,
                 CellValue = new CellValue("")
             }
           );

            rowfooter.Append(
                new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(listData.Sum(x=>x.Total))
                }
            );
          
            sheetData.Append(rowfooter);
            indexloop++;
            document.Save();
        }

        return pathFile;
    }




}