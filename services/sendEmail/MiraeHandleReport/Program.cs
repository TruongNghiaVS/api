namespace MiraeHandleReport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var fileImport = new HanleFileExcel();
            var fileImport2 = new HandleFileWorkingTime();
            var timeRunloop = new DateTime(2025, 01, 01, 0, 0, 0);
            //while (timeRunloop <= DateTime.Now.AddDays(-1))
            //{
            //    var timerun = timeRunloop.Date;
            //    fileImport.OutPutFile(timerun);
            //    timeRunloop = timeRunloop.AddDays(1);
            //}
            while (timeRunloop <= DateTime.Now.AddDays(-1))
            {
                var timerun = timeRunloop.Date;
                fileImport2.OutputFileWorkingTime(timerun);
                timeRunloop = timeRunloop.AddDays(1);
            }
        }
    }
}
