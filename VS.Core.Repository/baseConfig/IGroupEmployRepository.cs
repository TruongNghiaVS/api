using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Repository.baseConfig
{
    public interface IGroupEmployRepository : IGenericRepository<GroupEmployee>
    {
        Task<int> AddMemberGroup(GroupMember entity);

        Task<int> Deletemember(int id);




        Task<bool> CheckDuplicate(string codeCheck);
        Task<GroupEmployeeReponse> GetALl(GroupEmployeeRequest request);
        Task<GroupEmployeeHaveNotInGroupRequestReponse> GetAllMeberHaveNotGroup(GroupEmployeeHaveNotInGroupRequest request);

        Task<EmployeeSearchReponse> getMemberByGroup(MemberGroupByIdRequest request);
        Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request = null);
    }

}
