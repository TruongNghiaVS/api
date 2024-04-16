namespace VS.Core.Repository.Model
{
    public class CrmReprotIndexModel
    {


        public string Id { get; set; }
        public CrmReprotIndexModel()
        {

        }
        public string ManagerName { get; set; }

        public string ManagerFullName { get; set; }
        public string STT { get; set; }
        public string LineCode { get; set; }

        public string FullName { get; set; }

        public string SumNoAgree { get; set; }
        public string SumCall { get; set; }

        public Double Timcall { get; set; }

        public Double TimeWaiting { get; set; }

        public Double TimeTalking { get; set; }


        public int YearR { get; set; }

        public int MonthR { get; set; }

        public int DayR { get; set; }
       

    }



}
