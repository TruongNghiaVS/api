﻿using VS.core.Request;
using VS.Core.dataEntry.User;
using VS.Core.Repository.Model;

namespace VS.Core.Business.Interface
{
    public interface IGroupEmpBussiness : IGenericBussine<GroupEmployee>
    {
        Task<GroupEmployee> Getbyid(string Id);

        Task<List<SelectIndexModel>> GetAllManager(GroupEmployeeRequest? request);
        Task<bool> CheckDuplicate(string code);
        Task<GroupEmployeeReponse> GetALl(GroupEmployeeRequest request);
        Task<EmployeeSearchReponse> getMemberByGroup(MemberGroupByIdRequest request);
        Task<int> AddMemberGroup(GroupMember entity);
        Task<int> Deletemember(int id);
        Task<GroupEmployeeHaveNotInGroupRequestReponse> GetAllMeberHaveNotGroup(GroupEmployeeHaveNotInGroupRequest request);


    }
}
