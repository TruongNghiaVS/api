
using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class CampagnBusiness : BaseBusiness, ICampagnBussiness
    {

        public CampagnBusiness(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public Task<int> AddAsync(Campagn entity)
        {
            return _unitOfWork.CampagnRe.AddAsync(entity);
        }

        public Task<bool> CheckDuplicate(string code)
        {
            return _unitOfWork.CampagnRe.CheckDuplicate(code);
        }

        public Task Delete(Campagn entity)
        {
            return _unitOfWork.CampagnRe.Delete(entity);
        }

        public Task<CampagnRequestReponse> GetALl(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetALl(request);
        }
        public Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetAllAsiggeeByCampagnId(request);
        }

        public Task<Campagn> Getbyid(string Id)
        {
            return _unitOfWork.CampagnRe.GetByIdAsync(Id);
        }

        public Task<Campagn> GetByIdAsync(string id)
        {
            return _unitOfWork.CampagnRe.GetByIdAsync(id);
        }

        public Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(Campagn entity)
        {

            return _unitOfWork.CampagnRe.UpdateAsyn(entity);
        }
        public Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(GetAllProfileByCampang request)
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }

        public Task<List<Profile>> GetALLAsiggnee(GetAllProfileByCampang request)
        {
            return _unitOfWork.CampagnProfileRe.GetALLAsiggnee(request);
        }


        public Task<int> UpdateProfile(Profile entity)
        {

            return _unitOfWork.CampagnProfileRe.UpdateAsyn(entity);
        }

        public Task<int> AddProfile(Profile entity)
        {
            return _unitOfWork.CampagnProfileRe.AddAsync(entity);
        }

        public Task<Profile> GetProfile(string id)
        {
            return _unitOfWork.CampagnProfileRe.GetByIdAsync(id);
        }
        public Task DeleteProfile(Profile entity)
        {
            return _unitOfWork.CampagnProfileRe.Delete(entity);
        }

        public async Task<bool> HandleImport(CampanginDataImportRequest request)
        {
            var id = request.Id;
            var listData = request.ListData;
            foreach (var item in listData)
            {

                var itemInsert = new Profile
                {
                    CustomerName = item.CustomerName,
                    NoAgreement = item.NoAgreement,
                    DayOfBirth = DateTime.Now,
                    NationalId = item.NationalId,
                    MobilePhone = item.MobilePhone,
                    Phone1 = item.Phone1,
                    AmountLoan = item.AmountLoan,
                    Assignee = item.Assignee,
                    CampaignId = item.CampaignId,
                    CodeProduct = item.CodeProduct,
                    CreateAt = item.CreateAt,
                    CreatedBy = item.CreatedBy,
                    DebitOriginal = item.DebitOriginal,
                    Deleted = item.Deleted,
                    DPD = item.DPD,
                    Email = item.Email,
                    EMI = item.EMI,
                    HouseNumber = item.HouseNumber,
                    Id = item.Id,
                    LastPadDay = item.LastPadDay,
                    LastPaid = item.LastPaid,
                    NameProduct = item.NameProduct,
                    NoteFirstTime = item.NoteFirstTime,
                    NoTenure = item.NoTenure,
                    OfficeNumber = item.OfficeNumber,
                    OtherPhone = item.OtherPhone,
                    PriceProduct = item.PriceProduct,
                    Provice = item.Provice,
                    Provice1 = item.Provice1,
                    Provice2 = item.Provice2,
                    RegisterDay = item.RegisterDay,
                    Road = item.Road,
                    Road1 = item.Road1,
                    Road2 = item.Road2,
                    StatusPayMent = item.StatusPayMent,
                    SuburbanDir = item.SuburbanDir,
                    SuburbanDir1 = item.SuburbanDir1,
                    SuburbanDir2 = item.SuburbanDir2,
                    Tenure = item.Tenure,
                    TotalFines = item.TotalFines,
                    TotalMoneyPaid = item.TotalMoneyPaid,
                    TotalPaid = item.TotalPaid,
                    UpdateAt = item.UpdateAt,
                    UpdatedBy = item.UpdatedBy

                };
                await _unitOfWork.CampagnProfileRe.AddAsync(itemInsert);

            }

            var campaign = await _unitOfWork.CampagnRe.GetByIdAsync(request.Id);
            campaign.SumCount = listData.Count;
            campaign.ProcessingCount = 0;
            campaign.ClosedCount = 0;
            campaign.UpdateAt = DateTime.Now;
            await UpdateAsyn(campaign);
            return true;
        }


    }
}
