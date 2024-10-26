namespace VS.core.AutoCall
{
    public class DataItem
    {
        public string Phone { get; set; }

        public string NoAggree { get; set; }

        public int Id { get; set; }

    }
    public sealed class DataCall
    {
        public DataCall()
        {

            DataCalls = new List<DataItem>();
            DataCalled = new List<DataItem>();
        }
        private static DataCall instatances = null;
        public List<DataItem> DataCalls { get; set; }
        public List<DataItem> DataCalled { get; set; }
        public static DataCall GetInstances
        {
            get
            {
                if (instatances == null)
                {
                    instatances = new DataCall();
                }
                return instatances;
            }
        }
    }
}
