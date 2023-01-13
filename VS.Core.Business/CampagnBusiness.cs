

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
        public async Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(CampagnRequest request)
        {
            var result = await _unitOfWork.CampagnRe.GetAllAsiggeeByCampagnId(request);
            var campagnOverviewByid = await _unitOfWork.CampagnRe.GetOverviewCampagnById(request.CampaignId);

            if (campagnOverviewByid != null)
            {
                result.Model = campagnOverviewByid.model;
            }
            else result.Model = new GetOverviewCampaignModelById();
            return result;

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

        public async Task<bool> HandleImport(CampanginDataImportRequest request, Account userLogin)
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
                var result = await _unitOfWork.CampagnProfileRe.GetByNoAgreement(item.NoAgreement);
                if (result != null)
                {
                    result.CustomerName = item.CustomerName;
                    result.NoAgreement = item.NoAgreement;
                    result.DayOfBirth = DateTime.Now;
                    result.NationalId = item.NationalId;
                    result.MobilePhone = item.MobilePhone;
                    result.Phone1 = item.Phone1;
                    result.AmountLoan = item.AmountLoan;
                    result.Assignee = item.Assignee;
                    result.CampaignId = item.CampaignId;
                    result.CodeProduct = item.CodeProduct;
                    result.CreateAt = item.CreateAt;
                    result.CreatedBy = item.CreatedBy;
                    result.DebitOriginal = item.DebitOriginal;
                    result.Deleted = item.Deleted;
                    result.DPD = item.DPD;
                    result.Email = item.Email;
                    result.EMI = item.EMI;
                    result.HouseNumber = item.HouseNumber;

                    result.LastPadDay = item.LastPadDay;
                    result.LastPaid = item.LastPaid;
                    result.NameProduct = item.NameProduct;
                    result.NoteFirstTime = item.NoteFirstTime;
                    result.NoTenure = item.NoTenure;
                    result.OfficeNumber = item.OfficeNumber;
                    result.OtherPhone = item.OtherPhone;
                    result.PriceProduct = item.PriceProduct;
                    result.Provice = item.Provice;
                    result.Provice1 = item.Provice1;
                    result.Provice2 = item.Provice2;
                    result.RegisterDay = item.RegisterDay;
                    result.Road = item.Road;
                    result.Road1 = item.Road1;
                    result.Road2 = item.Road2;
                    result.StatusPayMent = item.StatusPayMent;
                    result.SuburbanDir = item.SuburbanDir;
                    result.SuburbanDir1 = item.SuburbanDir1;
                    result.SuburbanDir2 = item.SuburbanDir2;
                    result.Tenure = item.Tenure;
                    result.TotalFines = item.TotalFines;
                    result.TotalMoneyPaid = item.TotalMoneyPaid;
                    result.TotalPaid = item.TotalPaid;
                    result.UpdateAt = item.UpdateAt;
                    result.UpdatedBy = item.UpdatedBy;
                    result.UpdatedBy = userLogin.Id;
                    await _unitOfWork.CampagnProfileRe.UpdateAsyn(result);
                }
                else
                {
                    itemInsert.CreatedBy = userLogin.Id;
                    await _unitOfWork.CampagnProfileRe.AddAsync(itemInsert);
                }



            }

            var campaign = await _unitOfWork.CampagnRe.GetByIdAsync(request.Id);
            campaign.SumCount = listData.Count;
            campaign.ProcessingCount = 0;
            campaign.ClosedCount = 0;
            campaign.UpdateAt = DateTime.Now;
            await UpdateAsyn(campaign);
            return true;
        }
        public Task<bool> AssignedTask(string profileId, string userId)
        {
            return _unitOfWork.CampagnProfileRe.AssignedTask(profileId, userId);
        }
        public async Task<CampangeProfileInforReponse> GetIno(string id)
        {
            var result = await _unitOfWork.CampagnProfileRe.GetByIdAsync(id);
            var reponse = new CampangeProfileInforReponse()
            {

            };
            if (result == null)
            {

                return reponse;
            }

            reponse.campagn = await _unitOfWork.CampagnRe.GetByIdAsync(result.CampaignId.ToString());


            var allImpactHistory = await _unitOfWork.ImpactRe.GetALl(new ImpactHistorySerarchRequest()
            {
                ProfileId = id
            });

            var listReason = await _unitOfWork.MasterRe.GetALl(new MaterDataRequest()
            {
                GroupStatus = "6"
            });

            var listUser = await _unitOfWork.Employees.GetALl(new EmployeeSearchRequest()
            {

            });

            reponse.ListUser = listUser.Data;

            reponse.Result = result;

            var statusText = "Chưa rõ trạng thái";
            reponse.ListHistory = allImpactHistory.Data;

            if (result.Status == 0)
            {
                statusText = "Mới phân";
            }
            else if (result.Status == 1)
            {
                statusText = "Theo dõi";
            }
            else if (result.Status == 2)
            {
                statusText = "Đang đóng hồ sơ";
            }

            reponse.StatusProfile = statusText;
            reponse.ListReason = listReason.Data;
            return reponse;


        }

        public async Task<bool> HandleCase(CampaignProfile_caseRequest request)
        {
            return await _unitOfWork.CampagnProfileRe.HanldleCase(request.Id, request.ResetCase, request.Skipp);


        }
        public async Task<bool> UpdateOverViewAllCampagn()
        {
            var allCampang = await _unitOfWork.CampagnRe.GetALlCampang();
            foreach (var item in allCampang)
            {
                await _unitOfWork.CampagnRe.UpdateOverView(item.Id);
            }
            return true;
        }

    }
}
