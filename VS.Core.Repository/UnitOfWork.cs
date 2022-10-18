using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;

namespace VS.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; }
        public IUserRepository UserRe { get; }

        public IMasterDataRepository MasterRe { get; }
        public IGroupReasonRepository GroupRe { get; }
        public ICampagnRepository CampagnRe { get; }

        public IProfileCampagnRepository CampagnProfileRe { get; }

        public UnitOfWork(IEmployeeRepository employeeRepository,
             IUserRepository userRepository,
             IMasterDataRepository masterDataRepository,
             IGroupReasonRepository groupRe,
             ICampagnRepository campagnRepository,
             IProfileCampagnRepository campagnProfileRe
            )
        {
            Employees = employeeRepository;
            UserRe = userRepository;
            MasterRe = masterDataRepository;
            GroupRe = groupRe;
            CampagnRe = campagnRepository;
            CampagnProfileRe = campagnProfileRe;
        }

    }
}