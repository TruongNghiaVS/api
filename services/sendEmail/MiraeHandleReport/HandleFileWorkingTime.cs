using MiraeHandleReport.Model;
using OfficeOpenXml;

namespace MiraeHandleReport
{
    public class HandleFileWorkingTime
    {

        public readonly string FORTMAT_DATETIME = "mmmm d, yyyy, h:mm AM/PM";
        public List<DataWorkingTimeSource> LoadFileDataSorce()
        {
            var listData = new List<DataWorkingTimeSource>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pathInfo = "C:\\Users\\Admin\\Desktop\\source\\TC.xlsx";
            using (ExcelPackage package = new ExcelPackage(pathInfo))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.FirstOrDefault();
                if (workSheet != null)
                {
                    int totalRows = workSheet.Rows.Count();
                    for (int i = 2; i <= totalRows; i++)
                    {
                        var item = new DataWorkingTimeSource();
                        item.UserName = workSheet.Cells[i, 3].Value?.ToString();
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
            return DateTime.FromOADate(int.Parse(dayText));
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


        public static DateTime GetRandomDateTime(DateTime? min = null, DateTime? max = null)
        {
            Random rnd = new Random();
            var range = max.Value - min.Value;
            var randomUpperBound = (Int32)range.TotalSeconds;
            if (randomUpperBound <= 0)
                randomUpperBound = rnd.Next(1, Int32.MaxValue);

            var randTimeSpan = TimeSpan.FromSeconds((Int64)(range.TotalSeconds - rnd.Next(0, randomUpperBound)));
            return min.Value.Add(randTimeSpan);
        }
        public void OutputFileWorkingTime(DateTime? timerun)
        {
            if (timerun == null || timerun.HasValue == false)
            {
                timerun = DateTime.Now.AddDays(-1);
            }
            var timeNow = timerun.Value;

            var listDataHandle = LoadFileDataSorce();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pathInfo = "C:\\Users\\Admin\\Desktop\\source\\workingTime";
            var dateGet = timeNow.ToString("yyyyMMdd");
            var fileName = "working_time_" + dateGet + ".xlsx";
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
                sheet.Cells[1, 2].Value = "CHECK_IN";
                sheet.Cells[1, 3].Value = "CHECK_OUT";
                sheet.Cells[1, 4].Value = "DURATION_IN_HOUR";
                sheet.Cells[1, 5].Value = "WEEK_DAY";
                var indexLoop = 2;
                foreach (var item in listDataHandle)
                {
                    sheet.Cells[indexLoop, 1].Value = item.UserName;
                    var day = timeNow.Day;
                    var month = timeNow.Month;
                    var year = timeNow.Year;
                    var timeLoginStand = new DateTime(year, month, day, 08, 30, 00);
                    var timeLogoutStand = new DateTime(year, month, day, 17, 30, 00);
                    var timeLogin = GetRandomDateTime(timeLoginStand.AddMinutes(-10), timeLoginStand.AddMinutes(2));
                    var timeLogout = GetRandomDateTime(timeLogoutStand.AddMinutes(-3), timeLogoutStand.AddMinutes(5));

                    var hours = (timeLogout - timeLogin).TotalHours;
                    hours = Math.Round(hours, 1);
                    sheet.Cells[indexLoop, 2].Style.Numberformat.Format = FORTMAT_DATETIME;
                    sheet.Cells[indexLoop, 2].Value = timeLogin;
                    sheet.Cells[indexLoop, 3].Style.Numberformat.Format = FORTMAT_DATETIME;
                    sheet.Cells[indexLoop, 3].Value = timeLogout;
                    sheet.Cells[indexLoop, 4].Value = hours;
                    sheet.Cells[indexLoop, 5].Value = timeNow.DayOfWeek.ToString();
                    indexLoop++;
                }
                sheet.Cells.AutoFitColumns();
                package.Save();
            }

        }


    }
}
