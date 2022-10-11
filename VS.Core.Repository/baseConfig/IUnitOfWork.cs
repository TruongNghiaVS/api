using VS.Core.Repository.Interface;

namespace VS.Core.Repository.baseConfig
{
    public interface IUnitOfWork
    {

        IEmployeeRepository Employees { get; }
        IUserRepository UserRe { get; }
        IMasterDataRepository MasterRe { get; }
        IGroupReasonRepository GroupRe { get; }

    }
}
