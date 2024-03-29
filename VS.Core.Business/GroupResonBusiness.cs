﻿using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class GroupResonBusiness : BaseBusiness, IGroupResonBussiness
    {

        public GroupResonBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> Add(GroupReason entity)
        {
            return _unitOfWork.GroupRe.Add(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.GroupRe.CheckDuplicate(code);
        }

        public Task Delete(GroupReason entity)
        {
            return _unitOfWork.GroupRe.Delete(entity);
        }

        public Task<GroupReasonReponse> GetALl(GroupReasonRequest request)
        {
            return _unitOfWork.GroupRe.GetALl(request);
        }

        public Task<ReasonReponse> getAllStatus(int? vendorId, int? userId)
        {
            return _unitOfWork.GroupRe.getAllStatus(vendorId, userId);
        }

        public Task<GroupReason> Getbyid(string Id)
        {
            return _unitOfWork.GroupRe.GetById(Id);
        }

        public Task<GroupReason> GetByIdAsync(string id)
        {
            return _unitOfWork.GroupRe.GetById(id);
        }

        public Task<GroupReasonReponse> GetDataForExport(GroupReasonRequest request)
        {
            return _unitOfWork.GroupRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(GroupReason entity)
        {
            return _unitOfWork.GroupRe.Update(entity);
        }
    }
}
