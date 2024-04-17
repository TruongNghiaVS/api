namespace VS.core.API.Global
{

    public class UserLoginInDay
    {

        public string UserName { get; set; }

        public DateTime?  LoginAt { get; set; }




    }
    public class UserContainer
    {

        private static UserContainer _instance;

        public List<UserLoginInDay> DataUser { get; set; }
        public UserContainer()
        {
            DataUser = new List<UserLoginInDay>();
        }

        public static UserContainer GlobalContainer()
        {
       

            if (_instance == null)
            {
                _instance = new UserContainer();
            }

            return _instance;
        }

        public void AddUser(string UserName)
        {
            var item = DataUser.Where(x => x.UserName == UserName).FirstOrDefault();
            if(item == null)
            {

            }
            else
            {
                DataUser.Remove(item);
            }
            DataUser.Add(new UserLoginInDay()
            {
                UserName = UserName,
                LoginAt = DateTime.Now
            });
               

        }

        public bool ValidAcess(string _userName)
        {
             if(string.IsNullOrEmpty(_userName))
            {
                return false;
            }
            var item = DataUser.Where(x=>x.UserName== _userName).FirstOrDefault();
            if(item == null)
            {
                //AddUser(_userName);
                return false;
            }
            return true;
        }

        public void ClearData ()
        {
            DataUser.Clear();
        }

    }
}
