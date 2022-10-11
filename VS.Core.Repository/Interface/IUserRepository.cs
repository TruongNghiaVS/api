using VS.Core.dataEntry.User;

namespace VS.Core.Repository.Interface
{
    public interface IUserRepository
    {
        Task<Account> Login(string userName, string password);

        Task<Account> GetById(string Id);

        Task<bool> InsertOrUpdate(Account account);
    }

}
