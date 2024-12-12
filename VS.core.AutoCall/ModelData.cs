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
            LineActive = new List<LineCall>();
            LineStop = new List<LineCall>();
            LimitChanel = 30;
            DataRing = new List<StatusItem>();
            LoadSip = false;
        }
        public List<PhoneLog> DataCall { get; set; }
        public Queue<PhoneLog> DataCallQueue { get; set; }

        public bool LoadSip { get; set; }
        public List<PhoneLog> DataCalled { get; set; }
        public bool IsTurnOfAutocall { get; set; }
        public List<LineCall> LineActive { get; set; }
        public List<LineCall> LineStop { get; set; }


        public void StopAutoCall(string lineCode)
        {
            var itemRemove = LineActive.Where(x => x.Line == lineCode).FirstOrDefault();
            if (itemRemove == null)
            {
                return;
            }
            LineActive.Remove(itemRemove);
        }

        public void AddAutoCall(string lineCode)
        {
            var itemRemove = LineActive.Where(x => x.Line == lineCode).FirstOrDefault();
            if (itemRemove == null)
            {
                itemRemove.LastUpdate = DateTime.Now;
                return;
            }
            LineActive.Add(itemRemove);
        }

        public void SetLineCalling(string lineCode)
        {
            var itemCall = LineActive.Where(x => x.Line == lineCode).FirstOrDefault();
            if (itemCall == null)
            {
                return;
            }
            itemCall.LastUpdate = DateTime.Now;
            itemCall.Iscall = true;
        }
        public void StopLineCall(string lineCode)
        {
            var itemCall = LineActive.Where(x => x.Line == lineCode).FirstOrDefault();
            if (itemCall == null)
            {
                return;
            }
            itemCall.LastUpdate = DateTime.Now;
            itemCall.Iscall = false;
        }

        public List<LineCall> ListLineCall()
        {
            return LineActive.Where(x => x.Iscall == false)
                            .OrderBy(x => x.LastUpdate)
                            .ToList();
        }
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


        public List<StatusItem> DataRing { get; set; }
        public int CountDataRing
        {
            get
            {
                return DataRing.Count();
            }
        }

        public int LimitChanel { get; set; }
        public void SetIndexRow()
        {
            indexRow++;

        }
    }
}
