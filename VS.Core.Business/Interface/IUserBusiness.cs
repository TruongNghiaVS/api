using VS.Core.dataEntry.User;

namespace VS.Core.Business.Interface
{
    public interface IUserBusiness
    {

        Task<Account> Login(string userName, string password);
    }
}
