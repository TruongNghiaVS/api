using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;
using Microsoft.Office.Interop.Excel;
using System;
using VS.Core.Repository.Model;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Crypto.Agreement;
using System.Net.WebSockets;
using System.Net.Http;
namespace VS.Core.Business
{

    
    public class ExportFileBussiness : IExportFileBussiness
    {
        private readonly IUnitOfWork _unitOfWork1;
        public ExportFileBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public async  Task<bool> HandleExportFile(CampagnProfileExportRequest request, string userName)
        {
         
            try
            {
             
                Microsoft.Office.Interop.Excel.Application excelApp;
                Microsoft.Office.Interop.Excel._Workbook oWB;
                Microsoft.Office.Interop.Excel._Worksheet workSheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Visible = true;

                oWB = (Microsoft.Office.Interop.Excel._Workbook)(excelApp.Workbooks.Add(""));
                workSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                int pageBatch = 0;
                

                workSheet.Cells[1,1] = "CONTRACT NUMBER\r\nHợp đồng";

                workSheet.Cells[1, 2] = "Customer name\r\nKhách hàng 3";
                workSheet.Cells[1, 3] = "BOD 4";
                workSheet.Cells[1, 4] = "ID card no";
                workSheet.Cells[1, 5] = "Date of sign 6";
                workSheet.Cells[1, 6] = "PRODUCTCODE\r\nMã sản phẩm";
                workSheet.Cells[1, 7] = "Product name\r\nTên sản phẩm 7";
                workSheet.Cells[1, 8] = "VIN\r\nSố khung";
                workSheet.Cells[1, 9] = "MRC Plate\r\nBiển số xe";
                workSheet.Cells[1, 10] = "Product Price\r\nGiá sản phẩm";
                workSheet.Cells[1, 11] = "Down payment\r\nSố tiền trả trước";
                workSheet.Cells[1, 12 ] = "Amount of credit\r\nTiền vay 9";
                workSheet.Cells[1, 13 ] = "Sum of payment\r\nTổng tiền phải trả 14";
                workSheet.Cells[1, 14] = "Installment\r\nSố kỳ phải trả 10";
                workSheet.Cells[1, 15] = "Monthly payment\r\nSố tiền trả theo kỳ 11";
                workSheet.Cells[1, 16] = "First installment\r\nNgày trả đầu tiên";
                workSheet.Cells[1, 17] = "Last installment\r\nNgày trả cuối cùng";
                workSheet.Cells[1, 18] = "Count of payment\r\nSố kỳ đã trả";
                workSheet.Cells[1, 19] = "Sum of penalty\r\nTổng tiền phạt";
                workSheet.Cells[1, 20] = "Sum of penalty paid\r\nTiền phạt đã trả";
                workSheet.Cells[1, 21] = "The rest of penalty\r\nTiền phạt còn lại";
                workSheet.Cells[1, 22] = "Date of last payment";
                workSheet.Cells[1, 23] = "Total paid\r\nSố tiền đã trả";
                workSheet.Cells[1, 24] = "Total debt\r\nSố nợ hiện tại 21";
                workSheet.Cells[1, 25] = "Current DPD 15";
                workSheet.Cells[1, 26] = "MOBIPHONE\r\nSDT KH 16";
                workSheet.Cells[1, 27] = "HOMEPHONE\r\nSDT nhà";
                workSheet.Cells[1, 28] = "REL2_PHONE\r\nSDT người thân 1";
                workSheet.Cells[1, 29] = "REL2_PHONE\r\nSDT người thân 2";
                workSheet.Cells[1, 30] = "WORKING_PHONE\r\nSDT nơi làm việc";
                workSheet.Cells[1, 31] = "Permanent address\r\nĐ/c thường trú 18";
                workSheet.Cells[1, 32]= "Permanent district\r\nQuận thường trú 19";
                workSheet.Cells[1, 33] = "Permanent Region\r\nKhu vực thường trú 20";
                workSheet.Cells[1, 34] = "Contact address\r\nĐ/c liên lạc";
                workSheet.Cells[1, 35] = "Contact district\r\nQuận liên lạc";
                workSheet.Cells[1, 36] = "Region\r\nKhu vực";
                workSheet.Cells[1, 37] = "Name of employer\r\nNghề nghiệp 9";
                workSheet.Cells[1, 38] = "GHI CHÚ BAN ĐẦU";
                workSheet.Cells[1, 39] = "Tham Chiếu Rel info 17";
                workSheet.Cells[1, 40] = "id 39";
                workSheet.Cells[1, 41] = "Action Code";
                workSheet.Cells[1, 42] = "Phương thức liên hệ";
                workSheet.Cells[1, 43] = "Nơi liên hệ";
                workSheet.Cells[1, 44] = "Phân loại hồ sơ";
                bool isContinue = true;
                int indexloop = 1;
                do
                {
                    pageBatch++;
                    var requestQuery = new CampagnProfileExportRequest()
                    {
                        CampaignId = request.CampaignId,
                        From = request.From,
                        To = request.To,
                        Limit = 500,
                        Page = pageBatch,
                        VendorId = request.VendorId
                    };
                    var resultData = await _unitOfWork1.ExportFile.GetAllCampagnProfile(requestQuery);
                    isContinue = (resultData.NumberRecord > 0);
               

                    //lưu file excel
                    var data = resultData.Data;
                    
                    foreach (var item in data)
                    {
                        indexloop++;
                        var itemexport = item as CampagnProileExportIndexModel;
                        workSheet.Cells[indexloop, 1] = itemexport.NoAgreement;
                        //workSheet.Cells[indexloop, 2] = itemexport.CustomerName;
                        //workSheet.Cells[indexloop, 3] = itemexport.DPD;
                        //workSheet.Cells[indexloop, 4] = itemexport.NationalId;
                        //workSheet.Cells[indexloop, 5] = itemexport.RegisterDay;
                        //workSheet.Cells[indexloop, 6] = itemexport.CodeProduct;
                        //workSheet.Cells[indexloop, 7] = itemexport.NameProduct;
                        //workSheet.Cells[indexloop, 12] = itemexport.AmountLoan;
                        //workSheet.Cells[indexloop, 13] = itemexport.TotalMoneyPaid;

                        //workSheet.Cells[indexloop, 14] = itemexport.Tenure;
                        //workSheet.Cells[indexloop, 15] = itemexport.EMI;
                        //workSheet.Cells[indexloop, 17] = itemexport.LastPadDay;

                        //workSheet.Cells[indexloop, 18] = itemexport.NoTenure;
                        //workSheet.Cells[indexloop, 19] = itemexport.TotalFines;
                        //workSheet.Cells[indexloop, 23] = itemexport.TotalPaid;
                        //workSheet.Cells[indexloop, 24] = itemexport.DebitOriginal;
                        //workSheet.Cells[indexloop, 25] = itemexport.DPD;
                        //workSheet.Cells[indexloop, 26] = itemexport.MobilePhone;

                        //workSheet.Cells[indexloop, 27] = itemexport.HouseNumber;
                        //workSheet.Cells[indexloop, 28] = itemexport.Phone1;
                        //workSheet.Cells[indexloop, 29] = itemexport.OtherPhone;
                        //workSheet.Cells[indexloop, 30] = itemexport.OfficeNumber;

                        //workSheet.Cells[indexloop, 31] = itemexport.Road;
                        //workSheet.Cells[indexloop, 32] = itemexport.SuburbanDir;
                        //workSheet.Cells[indexloop, 33] = itemexport.Provice;
                        //workSheet.Cells[indexloop, 34] = itemexport.Road1;
                        //workSheet.Cells[indexloop, 35] = itemexport.SuburbanDir1;
                        //workSheet.Cells[indexloop, 36] = itemexport.Provice1;

                        //workSheet.Cells[indexloop, 38] = itemexport.NoteFirstTime;
                        //workSheet.Cells[indexloop, 39] = itemexport.NoteRel;
                        //workSheet.Cells[indexloop, 40] = itemexport.AssignedId;
                        //var actionCode = itemexport.Code;
                        //if(!string.IsNullOrEmpty(actionCode))
                        //{
                        //    workSheet.Cells[indexloop, 41] = itemexport.Code;
                        //    workSheet.Cells[indexloop, 42] = itemexport.WayContact;
                        //    workSheet.Cells[indexloop, 43] = itemexport.PlaceCode;
                        //    workSheet.Cells[indexloop, 44] = itemexport.ColorCode;
                        //}
                        //else
                        //{
                        //    workSheet.Cells[indexloop, 41] = "Mới";
                        //    workSheet.Cells[indexloop, 42] = "";
                        //    workSheet.Cells[indexloop, 43] = "";
                        //    workSheet.Cells[indexloop, 44] = "";
                        //} 
                            
                     
                     

                    }

                }
                while (isContinue);
                var dateGet = DateTime.Now;
                var fileName = "reportCase" + userName + dateGet.ToString("dd.MM.yy") + ".xlsx";
                var folderName = userName;
                var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\report";
                var pathFolder = Path.Combine(rootPath, folderName);
                bool exists = System.IO.Directory.Exists(pathFolder);

                if (!exists)
                    System.IO.Directory.CreateDirectory(pathFolder);

              
                var pathFile = Path.Combine(pathFolder, fileName);
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
                oWB.SaveAs(pathFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, System.Type.Missing, System.Type.Missing,
                     false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                     System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);
                
                oWB.Close();
                excelApp.Quit();

            }
            catch (Exception e)
            {

             
            }
            
            return true;

        }


        public async Task<bool> HandleExportFile2(CampagnProfileExportRequest request, string userName)
        {

            try
            {

                Application excelApp;
               _Workbook oWB;
                _Worksheet workSheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;
                excelApp = new Application();
                excelApp.Visible = true;

                oWB = (excelApp.Workbooks.Add(""));
                workSheet = (_Worksheet)oWB.ActiveSheet;
                int pageBatch = 0;


                workSheet.Cells[1, 1] = "CONTRACT NUMBER\r\nHợp đồng";
                workSheet.Cells[1, 2] = "id 39";
                workSheet.Cells[1, 3] = "Action Code";
                workSheet.Cells[1, 4] = "Phương thức liên hệ";
                workSheet.Cells[1, 5] = "Nơi liên hệ";
                workSheet.Cells[1, 6] = "Phân loại hồ sơ";
                bool isContinue = true;
                int indexloop = 1;
                do
                {
                    pageBatch++;
                    var requestQuery = new CampagnProfileExportRequest()
                    {
                        CampaignId = request.CampaignId,
                        From = request.From,
                        To = request.To,
                        Limit = 500,
                        Page = pageBatch,
                        VendorId = request.VendorId
                    };
                    var resultData = await _unitOfWork1.ExportFile.GetAllCampagnProfile(requestQuery);
                    isContinue = (resultData.NumberRecord > 0);
                    isContinue = false;

                    //lưu file excel
                    var data = resultData.Data;

                    foreach (var item in data)
                    {
                        indexloop++;
                        var itemexport = item as CampagnProileExportIndexModel;
                        workSheet.Cells[indexloop, 1] = itemexport.NoAgreement;

                        workSheet.Cells[indexloop, 2] = itemexport.Assignee;
                        var actionCode = itemexport.Code;
                        if (!string.IsNullOrEmpty(actionCode))
                        {
                            workSheet.Cells[indexloop, 3 ] = itemexport.Code;
                            workSheet.Cells[indexloop, 4] = itemexport.WayContact;
                            workSheet.Cells[indexloop, 5] = itemexport.PlaceCode;
                            workSheet.Cells[indexloop, 6] = itemexport.ColorCode;
                        }
                        else
                        {
                            workSheet.Cells[indexloop, 3] = "Mới";
                            workSheet.Cells[indexloop, 4] = "";
                            workSheet.Cells[indexloop, 5] = "";
                            workSheet.Cells[indexloop, 6] = "";
                        }




                    }

                }
                while (isContinue);
                var dateGet = DateTime.Now;
                var fileName = dateGet.ToString("dd.MM.yy") + ".xlsx";
                var folderName = userName;
                var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\DaillyReport";
                var pathFolder = Path.Combine(rootPath, folderName);
                bool exists = System.IO.Directory.Exists(pathFolder);

                if (!exists)
                    System.IO.Directory.CreateDirectory(pathFolder);


                var pathFile = Path.Combine(pathFolder, fileName);
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
                oWB.SaveAs(pathFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, System.Type.Missing, System.Type.Missing,
                     false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                     System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);

                oWB.Close();
                excelApp.Quit();

            }
            catch (Exception e)
            {


            }

            return true;

        }

        public async Task<string> HandleExportFile3(CampagnProfileExportRequest request, string userName)
        {

            try
            {

                //Application excelApp;
                _Workbook oWB;
                _Worksheet workSheet;
                Microsoft.Office.Interop.Excel.Range oRng;
                object misvalue = System.Reflection.Missing.Value;
                //excelApp = new Application();
                //excelApp.Visible = true;

                //oWB = (excelApp.Workbooks.Add(""));
                oWB = new Microsoft.Office.Interop.Excel.Workbook();
                workSheet = (_Worksheet)oWB.ActiveSheet;
                int pageBatch = 0;


                workSheet.Cells[1, 1] = "CONTRACT NUMBER\r\nHợp đồng";
                workSheet.Cells[1, 2] = "id 39";
                workSheet.Cells[1, 3] = "Action Code";
                workSheet.Cells[1, 4] = "Phương thức liên hệ";
                workSheet.Cells[1, 5] = "Nơi liên hệ";
                workSheet.Cells[1, 6] = "Phân loại hồ sơ";
                bool isContinue = true;
                int indexloop = 1;
                do
                {
                    pageBatch++;
                    var requestQuery = new CampagnProfileExportRequest()
                    {
                        CampaignId = request.CampaignId,
                        From = request.From,
                        To = request.To,
                        Limit = 500,
                        Page = pageBatch,
                        VendorId = request.VendorId
                    };
                    var resultData = await _unitOfWork1.ExportFile.GetAllCampagnProfile(requestQuery);
                    isContinue = (resultData.NumberRecord > 0);
                    isContinue = false;

                    //lưu file excel
                    var data = resultData.Data;

                    foreach (var item in data)
                    {
                        indexloop++;
                        var itemexport = item as CampagnProileExportIndexModel;
                        workSheet.Cells[indexloop, 1] = itemexport.NoAgreement;

                        workSheet.Cells[indexloop, 2] = itemexport.Assignee;
                        var actionCode = itemexport.Code;
                        if (!string.IsNullOrEmpty(actionCode))
                        {
                            workSheet.Cells[indexloop, 3] = itemexport.Code;
                            workSheet.Cells[indexloop, 4] = itemexport.WayContact;
                            workSheet.Cells[indexloop, 5] = itemexport.PlaceCode;
                            workSheet.Cells[indexloop, 6] = itemexport.ColorCode;
                        }
                        else
                        {
                            workSheet.Cells[indexloop, 3] = "Mới";
                            workSheet.Cells[indexloop, 4] = "";
                            workSheet.Cells[indexloop, 5] = "";
                            workSheet.Cells[indexloop, 6] = "";
                        }




                    }

                }
                while (isContinue);
                var dateGet = DateTime.Now;
                var fileName = dateGet.ToString("dd.MM.yy") + ".xlsx";
                var folderName = userName;
                var rootPath = "C:\\vietbank\\crm\\api\\vsrolAPI2022\\DaillyReport";
                var pathFolder = Path.Combine(rootPath, folderName);
                bool exists = System.IO.Directory.Exists(pathFolder);

                if (!exists)
                    System.IO.Directory.CreateDirectory(pathFolder);


                var pathFile = Path.Combine(pathFolder, fileName);
                if (File.Exists(pathFile))
                {
                    File.Delete(pathFile);
                }
                oWB.SaveAs(pathFile, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, System.Type.Missing, System.Type.Missing,
                     false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                     System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing, System.Type.Missing);

                oWB.Close();
                //excelApp.Quit();

            }
            catch (Exception e)
            {
                return e.InnerException + e.Message;

            }

            return "33";

        }

    }
}
