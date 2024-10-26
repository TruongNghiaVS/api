namespace VS.core.AutoCall
{

    public sealed class ModelData
    {
        private static ModelData instance = null;
        private static readonly object padlock = new object();

        ModelData()
        {
            DataCall = new List<PhoneLog>();
            DataCallQueue = new Queue<PhoneLog>();
            DataCalled = new List<PhoneLog>();
            IsTurnOfAutocall = true;
        }

        public List<PhoneLog> DataCall { get; set; }
        public Queue<PhoneLog> DataCallQueue { get; set; }

        public List<PhoneLog> DataCalled { get; set; }

        public bool IsTurnOfAutocall { get; set; }
        public static ModelData Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ModelData();
                    }
                    return instance;
                }
            }
        }

        public int indexRow { get; set; }

        public void SetIndexRow()
        {
            indexRow++;
        }
    }
}
