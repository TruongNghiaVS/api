namespace VS.Core.Business.GlobalClass
{

    public class DataCall
    {

        public string Phone { get; set; }
        public string NoAgree { get; set; }




    }
    public class DataCallContainer
    {

        private static DataCallContainer _instance;

        public List<DataCall> Data { get; set; }
        public DataCallContainer()
        {
            Data = new List<DataCall>();
        }

        public static DataCallContainer GlobalContainer()
        {


            if (_instance == null)
            {
                _instance = new DataCallContainer();
            }

            return _instance;
        }

        public void AddUser(string phone, string noAgree)
        {
            var iteminsert = new DataCall { Phone = phone, NoAgree = noAgree };
            Data.Add(iteminsert);

        }

        public bool ValidAcess(string _userName)
        {
            return true;
        }

        public async Task<DataCall> GetDataCall()
        {
            if (Data.Count < 1)
                return new DataCall();
            return Data[0];
        }

        public bool CheckValidCall()
        {
            return Data.Count > 0;
        }
        public void RemoveData(string phone, string noagree)
        {

            var item = Data.Where(x => x.Phone == phone && x.NoAgree == noagree).FirstOrDefault();
            if (item != null)
            {
                Data.Remove(item);
            }


        }
    }
}
