namespace MiraeHandleReport.Model
{
    public class DataWorkingTimeSource
    {
        public DataWorkingTimeSource()
        {

        }
        public string UserName { get; set; }
        public string TeamLead { get; set; }
    }


    public class DataWorkingTimeHandle
    {
        public string Check_In { get; set; }
        public string Check_Out { get; set; }
        public float Duration { get; set; }

        public string Week_day { get; set; }
        public string UserName { get; set; }
    }
}
