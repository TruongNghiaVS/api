using VS.Core.dataEntry.User;

namespace VS.Core.Repository.Model
{
    public class SelectIndexModel : Account
    {

        public string? DisplayName
        {
            get
            {
                return this.UserName + "-" + this.FullName;
            }
        }
        public int TotalRecord { get; set; }





    }
}
