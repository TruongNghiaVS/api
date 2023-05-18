using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class MasterDataBusiness : BaseBusiness, IMasterDataBussiness
    {

        public MasterDataBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Task<int> Add(MasterData entity)
        {
            return _unitOfWork.MasterRe.Add(entity);
        }

        public Task<bool> CheckDuplicate(string code, string vendorId = null)
        {
            return _unitOfWork.MasterRe.CheckDuplicate(code, vendorId);
        }

        public Task<MasterData> GetbyCode(string code, string vendorId = null)
        {
            return _unitOfWork.MasterRe.GetbyCode(code, vendorId);
        }


        public Task Delete(MasterData entity)
        {
            return _unitOfWork.MasterRe.Delete(entity);
        }

        public Task<MasterDataReponse> GetALl(MaterDataRequest request)
        {
            return _unitOfWork.MasterRe.GetALl(request);
        }

        public Task<MasterData> Getbyid(string Id)
        {
            return _unitOfWork.MasterRe.GetById(Id);
        }

        public Task<MasterData> GetByIdAsync(string id)
        {
            return _unitOfWork.MasterRe.GetById(id);
        }

        public Task<MasterDataReponse> GetDataForExport(MaterDataRequest request)
        {
            return _unitOfWork.MasterRe.GetDataForExport(request);
        }


        public async Task<bool> HandleImport(MasterDataImportRequest request, Account userLogin)
        {
            var id = request.Id;
            var listData = request.ListData;
            var _campagnImport = await _unitOfWork.GroupRe.GetById(request.Id);
            var vendorId = _campagnImport.VendorId;
            foreach (var item in listData)
            {


                var itemInsert = new MasterData
                {
                    Code = item.Code,
                    FullName = item.FullName,
                    DisplayName = item.DisplayName,
                    GroupId = int.Parse(id),
                    VendorId = vendorId

                };

                var result = await _unitOfWork.MasterRe.GetbyCode(itemInsert.Code, vendorId != null ? vendorId.ToString() : null);
                if (result != null)
                {



                    result.FullName = result.DisplayName = itemInsert.FullName;
                    result.UpdatedBy = userLogin.Id;

                    await _unitOfWork.MasterRe.Update(result);
                }
                else
                {

                    itemInsert.CreatedBy = userLogin.Id;
                    itemInsert.VendorId = vendorId;
                    itemInsert.GroupId = id != null ? int.Parse(id) : null;
                    await _unitOfWork.MasterRe.Add(itemInsert);
                }



            }

            return true;
        }


        public Task<int> UpdateAsyn(MasterData entity)
        {
            return _unitOfWork.MasterRe.Update(entity);
        }
    }
}
