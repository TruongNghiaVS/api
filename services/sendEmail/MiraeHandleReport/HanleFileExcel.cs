using MiraeHandleReport.Model;
using OfficeOpenXml;

namespace MiraeHandleReport
{
    public class HanleFileExcel
    {

        public readonly string FORTMAT_DATETIME = "mmmm d, yyyy, h:mm AM/PM";
        public List<DataReportCDRSource> LoadFileDataSorce(DateTime timerun)
        {
            var listData = new List<DataReportCDRSource>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pathInfo = "C:\\Users\\Admin\\Desktop\\source";
            var dateGet = timerun.ToString("yyyyMMdd");
            var fileName = "call_report_" + dateGet + ".xlsx";
            var fileInfo = Path.Combine(pathInfo, fileName);
            if (!File.Exists(fileInfo))
            {
                return listData;
            }
            //var file = new FileInfo(fileInfo);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
                if (workSheet != null)
                {
                    int totalRows = workSheet.Rows.Count();
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var item = new DataReportCDRSource();
                        item.UserName = workSheet.Cells[i, 1].Value?.ToString();
                        item.TeamLead = workSheet.Cells[i, 2].Value?.ToString();
                        item.Agreement = workSheet.Cells[i, 3].Value?.ToString();
                        item.ActionCode = workSheet.Cells[i, 4].Value?.ToString();
                        item.PromiseDate = workSheet.Cells[i, 5].Value?.ToString();
                        item.PromiseAmt = workSheet.Cells[i, 6].Value?.ToString();

                        item.Remark = workSheet.Cells[i, 7].Value?.ToString();
                        item.CallDate = workSheet.Cells[i, 8].Value?.ToString();
                        item.Time = Gettime(workSheet.Cells[i, 9].Value?.ToString());
                        item.Contact_Person = workSheet.Cells[i, 10].Value?.ToString();
                        listData.Add(item);
                    }
                }
            }
            return listData;
        }

        public DateTime? GetDateTime(string dayText)
        {

            if (string.IsNullOrEmpty(dayText))
            {
                return DateTime.MinValue;
            }

            DateTime? dateValue = null;
            try
            {
                dateValue = DateTime.FromOADate(int.Parse(dayText));
            }
            catch (Exception)
            {


                dateValue = DateTime.ParseExact(dayText, "dd/MM/yyyy", null);


            }
            return dateValue;
        }
        private string Gettime(string timeText)
        {

            if (string.IsNullOrEmpty(timeText))
            {
                return "";
            }
            try
            {
                var datetimeInput = DateTime.Parse(timeText);

                return datetimeInput.TimeOfDay.ToString();
            }
            catch (Exception e)
            {

                return "";
            }

        }
        public void OutPutFile(DateTime? timerun)
        {
            if (timerun == null || timerun.HasValue == false)
            {
                timerun = DateTime.Now.AddDays(-1);
            }
            var dateGet = timerun.Value.ToString("yyyyMMdd");
            var listDataHandle = LoadFileDataSorce(timerun.Value);
            var listData = new List<DataReportCDRSource>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pathInfo = "C:\\Users\\Admin\\Desktop\\source\\output";
            var fileName = "call_report_" + dateGet + ".xlsx";
            var fileInfo = Path.Combine(pathInfo, fileName);
            if (File.Exists(fileInfo))
            {
                File.Delete(fileInfo);
                return;
            }
            var file = new FileInfo(fileInfo);
            using (ExcelPackage package = new ExcelPackage(file))
            {
                var sheet = package.Workbook.Worksheets.Add("Sheet1");
                sheet.Cells[1, 1].Value = "USERNAME";
                sheet.Cells[1, 2].Value = "TEAM_LEAD";
                sheet.Cells[1, 3].Value = "AGREEMENT_ID";
                sheet.Cells[1, 4].Value = "ACTION_CODE";
                sheet.Cells[1, 5].Value = "PROMISE_DATE";
                sheet.Cells[1, 6].Value = "PROMISE_AMT";
                sheet.Cells[1, 7].Value = "REMARK";
                sheet.Cells[1, 8].Value = "CALL_DATE";
                sheet.Cells[1, 9].Value = "CONTACT_PERSON";
                var indexLoop = 2;
                foreach (var item in listDataHandle)
                {
                    sheet.Cells[indexLoop, 1].Value = item.UserName;
                    sheet.Cells[indexLoop, 2].Value = item.TeamLead;
                    sheet.Cells[indexLoop, 3].Value = item.Agreement;
                    sheet.Cells[indexLoop, 4].Value = item.ActionCode;

                    var temp = item.PromiseDate;
                    DateTime? promiseDateInput = DateTime.MinValue;
                    try
                    {
                        if (!string.IsNullOrEmpty(temp))
                        {
                            promiseDateInput = GetDateTime(item.PromiseDate);


                        }
                        else
                        {
                            promiseDateInput = DateTime.MinValue;
                        }
                    }
                    catch (Exception e)
                    {
                        try
                        {

                            var dateCall = DateTime.Parse(item.PromiseDate);
                            promiseDateInput = dateCall.Date;
                        }
                        catch (Exception)
                        {




                            try
                            {
                                promiseDateInput = DateTime.ParseExact(item.CallDate, "dd/MM/yyyy", null);

                            }
                            catch (Exception)
                            {

                                promiseDateInput = DateTime.MinValue;

                            }


                        }


                    }
                    if (promiseDateInput.Value != DateTime.MinValue)
                    {
                        sheet.Cells[indexLoop, 5].Style.Numberformat.Format = FORTMAT_DATETIME;
                        sheet.Cells[indexLoop, 5].Value = promiseDateInput.Value;
                    }
                    sheet.Cells[indexLoop, 6].Value = item.PromiseAmt;
                    sheet.Cells[indexLoop, 7].Value = item.Remark;
                    DateTime? calldate = DateTime.MinValue;
                    try
                    {
                        var dateCall = DateTime.Parse(item.CallDate);
                        var totalSencond = TimeSpan.Parse(item.Time).TotalSeconds;
                        dateCall = dateCall.Date;
                        if (totalSencond > 0)
                        {
                            calldate = dateCall.AddSeconds(totalSencond);
                        }
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            DateTime date = DateTime.ParseExact(item.CallDate, "dd/MM/yyyy", null);
                            var totalSencond = TimeSpan.Parse(item.Time).TotalSeconds;
                            date = date.Date;
                            if (totalSencond > 0)
                            {
                                calldate = date.AddSeconds(totalSencond);
                            }
                        }
                        catch (Exception)
                        {

                            calldate = DateTime.MinValue;

                        }


                    }
                    if (calldate.Value != DateTime.MinValue)
                    {
                        sheet.Cells[indexLoop, 8].Value = calldate.Value;
                        sheet.Cells[indexLoop, 8].Style.Numberformat.Format = FORTMAT_DATETIME;

                    }
                    sheet.Cells[indexLoop, 9].Value = item.Contact_Person;
                    indexLoop++;
                }
                sheet.Cells.AutoFitColumns();
                package.Save();

            }

        }


    }
}
