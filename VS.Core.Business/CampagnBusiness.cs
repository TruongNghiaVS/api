using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Business.Model;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class CampagnBusiness : BaseBusiness, ICampagnBussiness
    {
        public CampagnBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<int> Add(Campagn entity)
        {
            return _unitOfWork.CampagnRe.Add(entity);
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

        public Task<CampangnOverviewByIdReponse> GetOverViewDashboardById(
            CampangnOverviewByIdRequest request
        )
        {
            return _unitOfWork.CampagnRe.GetOverViewDashboardById(request);
        }

        //public Task<CampangnOverviewByIdReponse> getOverViewDashboardById(CampangnOverviewByIdRequest request)
        //{
        //    return _unitOfWork.CampagnRe.getOverViewDashboardById(request);
        //}


        public async Task<CampagnAsiggeeByCampagnIdReponse> GetAllAsiggeeByCampagnId(
            CampagnRequest request
        )
        {
            var result = await _unitOfWork.CampagnRe.GetAllAsiggeeByCampagnId(request);
            //var campagnOverviewByid = await _unitOfWork.CampagnRe.GetOverviewCampagnById(
            //    request.CampaignId
            //);

            //if (campagnOverviewByid != null)
            //{
            //    result.Model = campagnOverviewByid.model;
            //}
            //else
            //    result.Model = new GetOverviewCampaignModelById();
            return result;
        }

        public Task<Campagn> Getbyid(string Id)
        {
            return _unitOfWork.CampagnRe.GetById(Id);
        }

        public Task<Campagn> GetByIdAsync(string id)
        {
            return _unitOfWork.CampagnRe.GetById(id);
        }

        public Task<CampagnRequestReponse> GetDataForExport(CampagnRequest request)
        {
            return _unitOfWork.CampagnRe.GetDataForExport(request);
        }

        public Task<int> UpdateAsyn(Campagn entity)
        {
            return _unitOfWork.CampagnRe.Update(entity);
        }


        public Task<int> UpdateSkipData(CampagnProfile entity)
        {
            return _unitOfWork.CampagnProfileRe.UpdateSkipData(entity);
        }

        public Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(
            GetAllProfileByCampang request
        )
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }


        public Task<GetAllProfileByCampangReponse> GetAllOriginal(
       GetAllProfileByCampang request
        )
        {
            return _unitOfWork.CampagnProfileRe.GetALlProfileByCampaign(request);
        }

        public Task<GetAllProfileByCampangReponse> ExportDataByCampaign(GetAllProfileByCampang request)
        {
            return _unitOfWork.CampagnProfileRe.ExportDataByCampaign(request);
        }
        public Task<List<CampagnProfile>> GetALLAsiggnee(GetAllProfileByCampang request)
        {
            return _unitOfWork.CampagnProfileRe.GetALLAsiggnee(request);
        }

        public Task<int> UpdateProfile(CampagnProfile entity)
        {
            return _unitOfWork.CampagnProfileRe.Update(entity);
        }

        public Task<int> UpdateProfileSkip(CampagnProfile entity)
        {
            return _unitOfWork.CampagnProfileRe.UpdateSkip(entity);
        }

        public Task<int> AddProfile(CampagnProfile entity)
        {
            return _unitOfWork.CampagnProfileRe.Add(entity);
        }

        public Task<CampagnProfile> GetProfile(string id)
        {
            return _unitOfWork.CampagnProfileRe.GetById(id);
        }

        public Task DeleteProfile(CampagnProfile entity)
        {
            return _unitOfWork.CampagnProfileRe.Delete(entity);
        }

        public async Task<bool> HandleImport(CampanginDataImportRequest request, Account userLogin)
        {
            if (request.Updatedata == true)
            {
                return await UpdateData(request, userLogin);
            }
            var id = request.Id;
            var listData = request.ListData;
            var _campagnImport = await _unitOfWork.CampagnRe.GetById(id);
            var vendorId = _campagnImport.VendorId;
            foreach (var item in listData)
            {
                if ( string.IsNullOrEmpty(item.AssignedId))
                {
                    continue;
                }    
                var itemInsert = new CampagnProfile
                {
                    CustomerName = item.CustomerName,
                    NoAgreement = item.NoAgreement,
                    DayOfBirth = item.DayOfBirth,
                    NationalId = item.NationalId,
                    MobilePhone = item.MobilePhone,
                    Phone1 = item.Phone1,
                    AmountLoan = item.AmountLoan,
                    CampaignId = int.Parse(id),
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
                    NoteRel = item.NoteRel,
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
                var result = await _unitOfWork.CampagnProfileRe.GetByNoAgreement(
                    item.NoAgreement,
                    request.Id
                );
                if (result != null)
                {
                    string? assignedId = null;
                    if (result.Assignee != item.AssignedId)
                    {

                        assignedId = item.AssignedId;
                        result.Status = 0;
                        result.ColorCode = "#ffffff";



                    }
                    else
                    {

                        if (result.Status == 0)
                        {
                            assignedId = item.AssignedId;
                            result.Status = 0;
                        }
                        assignedId = result.Assignee;



                    }

                    result.Assignee = assignedId;
                    result.CustomerName = item.CustomerName;
                    result.NoAgreement = item.NoAgreement;
                    result.DayOfBirth = item.DayOfBirth;
                    result.NationalId = item.NationalId;
                    result.MobilePhone = item.MobilePhone;
                    result.Phone1 = item.Phone1;
                    result.AmountLoan = item.AmountLoan;
                   
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
                    result.NoteRel = item.NoteRel;
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

                    result.SkipData = item.SkipData;
                    await _unitOfWork.CampagnProfileRe.ImportUpdate(result);
                }
                else
                {
                    string? assignedId = null;
                    if (!string.IsNullOrEmpty(item.AssignedId))
                    {
                        assignedId = item.AssignedId;
                        itemInsert.Status = 0;
                    }
                    else
                    {
                        itemInsert.Status = 10;
                    }
                    itemInsert.CreatedBy = userLogin.Id;
                    itemInsert.Assignee = assignedId;
                    itemInsert.SkipData = item.SkipData;
                    itemInsert.VendorId = vendorId;
                    await _unitOfWork.CampagnProfileRe.Add(itemInsert);

                }
            }
            var campaign = await _unitOfWork.CampagnRe.GetById(request.Id);
            campaign.SumCount = listData.Count;
            campaign.ProcessingCount = 0;
            campaign.ClosedCount = 0;
            campaign.UpdateAt = DateTime.Now;
            await UpdateAsyn(campaign);
            return true;
        }


        public async Task<bool> UpdateData(CampanginDataImportRequest request, Account userLogin)
        {
            var id = request.Id;
            var listData = request.ListData;
            var _campagnImport = await _unitOfWork.CampagnRe.GetById(id);
            var vendorId = _campagnImport.VendorId;
            foreach (var item in listData)
            {
                var result = await _unitOfWork.CampagnProfileRe.GetByNoAgreement(
                    item.NoAgreement,
                    request.Id
                );
                if (result == null)
                {
                    continue;
                }
                result.CustomerName = item.CustomerName;

                result.DayOfBirth = item.DayOfBirth;

                result.MobilePhone = item.MobilePhone;
                result.Phone1 = item.Phone1;
                result.AmountLoan = item.AmountLoan;


                result.CodeProduct = item.CodeProduct;

                result.DebitOriginal = item.DebitOriginal;

                result.DPD = item.DPD;
                result.Email = item.Email;
                result.EMI = item.EMI;
                result.HouseNumber = item.HouseNumber;
                result.LastPadDay = item.LastPadDay;
                result.LastPaid = item.LastPaid;
                result.NameProduct = item.NameProduct;
                result.NoteFirstTime = item.NoteFirstTime;
                result.NoteRel = item.NoteRel;
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
                result.UpdateAt = DateTime.Now;
                result.UpdatedBy = userLogin.Id;

                await _unitOfWork.CampagnProfileRe.ImportUpdate(result);
            }

            return true;
        }

        public async Task<bool> HandleImportV2(CampanginDataImportRequest request, Account userLogin)
        {
            var id = request.Id;
            var listData = request.ListData;
            var _campagnImport = await _unitOfWork.CampagnRe.GetById(id);
            var vendorId = _campagnImport.VendorId;


            //var tiemInsert = "insert into CampaignProfile (dpd,NationalId,Skipp, Reasonstatus,Status, MobilePhone, Phone1,HouseNumber, OfficeNumber, OtherPhone,Email, DayOfBirth, Road,SuburbanDir, Provice,Road1,SuburbanDir1, Provice1,Road2,SuburbanDir2, Provice2,StatusPayMent,RegisterDay,DebitOriginal,AmountLoan,EMI,CampaignId,Assignee,TotalFines,TotalMoneyPaid,Tenure,NoTenure,TotalPaid,LastPaid,LastPadDay,NameProduct,CodeProduct,PriceProduct,NoteFirstTime,NoteRel,SkipContent,VendorId,CreatedBy,UpdatedBy,CreateAt,UpdateAt,Deleted,NoAgreement,CustomerName, DeleteRecord) values";
            //foreach (var item in listData)
            //{

            //    var result = await _unitOfWork.CampagnProfileRe.GetByNoAgreement(
            //        item.NoAgreement,
            //        request.Id
            //    );
            //    if (result != null)
            //    {
            //        string? assignedId = null;
            //        if (string.IsNullOrEmpty(item.AssignedId) || result.Assignee != item.AssignedId)
            //        {
            //            assignedId = result.Assignee;
            //            result.Status = 0;
            //        }
            //        else
            //        {
            //            assignedId = item.AssignedId;
            //            result.Status = 0;

            //        }

            //        result.Assignee = assignedId;
            //        result.CustomerName = item.CustomerName;
            //        result.NoAgreement = item.NoAgreement;
            //        result.DayOfBirth = item.DayOfBirth;
            //        result.NationalId = item.NationalId;
            //        result.MobilePhone = item.MobilePhone;
            //        result.Phone1 = item.Phone1;
            //        result.AmountLoan = item.AmountLoan;
            //        result.Assignee = assignedId;
            //        result.CampaignId = item.CampaignId;
            //        result.CodeProduct = item.CodeProduct;
            //        result.CreateAt = item.CreateAt;
            //        result.CreatedBy = item.CreatedBy;
            //        result.DebitOriginal = item.DebitOriginal;
            //        result.Deleted = item.Deleted;
            //        result.DPD = item.DPD;
            //        result.Email = item.Email;
            //        result.EMI = item.EMI;
            //        result.HouseNumber = item.HouseNumber;
            //        result.LastPadDay = item.LastPadDay;
            //        result.LastPaid = item.LastPaid;
            //        result.NameProduct = item.NameProduct;
            //        result.NoteFirstTime = item.NoteFirstTime;
            //        result.NoteRel = item.NoteRel;
            //        result.NoTenure = item.NoTenure;
            //        result.OfficeNumber = item.OfficeNumber;
            //        result.OtherPhone = item.OtherPhone;
            //        result.PriceProduct = item.PriceProduct;
            //        result.Provice = item.Provice;
            //        result.Provice1 = item.Provice1;
            //        result.Provice2 = item.Provice2;
            //        result.RegisterDay = item.RegisterDay;
            //        result.Road = item.Road;
            //        result.Road1 = item.Road1;
            //        result.Road2 = item.Road2;
            //        result.StatusPayMent = item.StatusPayMent;
            //        result.SuburbanDir = item.SuburbanDir;
            //        result.SuburbanDir1 = item.SuburbanDir1;
            //        result.SuburbanDir2 = item.SuburbanDir2;
            //        result.Tenure = item.Tenure;
            //        result.TotalFines = item.TotalFines;
            //        result.TotalMoneyPaid = item.TotalMoneyPaid;
            //        result.TotalPaid = item.TotalPaid;
            //        result.UpdateAt = item.UpdateAt;
            //        result.UpdatedBy = item.UpdatedBy;
            //        result.UpdatedBy = userLogin.Id;
            //        await _unitOfWork.CampagnProfileRe.ImportUpdate(result);
            //    }
            //    else
            //    {
            //        string? assignedId = null;
            //        if (!string.IsNullOrEmpty(item.AssignedId))
            //        {
            //            assignedId = item.AssignedId;
            //            itemInsert.Status = 0;
            //        }
            //        else
            //        {
            //            itemInsert.Status = 10;
            //        }
            //        itemInsert.CreatedBy = userLogin.Id;
            //        itemInsert.Assignee = assignedId;
            //        itemInsert.VendorId = vendorId;
            //        await _unitOfWork.CampagnProfileRe.Add(itemInsert);

            //    }
            //}
            var campaign = await _unitOfWork.CampagnRe.GetById(request.Id);
            campaign.SumCount = listData.Count;
            campaign.ProcessingCount = 0;
            campaign.ClosedCount = 0;
            campaign.UpdateAt = DateTime.Now;
            await UpdateAsyn(campaign);
            return true;
        }
        public async Task<bool> HandleImportSkip(CampanginDataImportRequest request, Account userLogin)
        {

            var id = request.Id;
            var listData = request.ListData;
            foreach (var item in listData)
            {
                var itemInsert = new CampagnProfile
                {
                    CustomerName = item.CustomerName,
                    NoAgreement = item.NoAgreement,
                    DayOfBirth = item.DayOfBirth,
                    NationalId = item.NationalId,
                    MobilePhone = item.MobilePhone,
                    Phone1 = item.Phone1,
                    AmountLoan = item.AmountLoan,
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
                    NoteRel = item.NoteRel,
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
                    UpdateAt = DateTime.Now,
                    SkipContent = item.SkipContent,
                    UpdatedBy = userLogin.Id
                };
                var result = await _unitOfWork.CampagnProfileRe.GetByNoAgreement(
                    item.NoAgreement
                );

                if (result == null)
                {
                    continue;
                }
                if (result != null)
                {
                    string? assignedId = null;
                    if (string.IsNullOrEmpty(item.AssignedId) || result.Assignee != item.AssignedId)
                    {
                        assignedId = result.Assignee;
                        result.Status = 0;
                    }
                    else
                    {
                        assignedId = item.AssignedId;
                        result.Status = 0;

                    }
                    result.Status = 0;

                    result.Assignee = assignedId;


                    result.DayOfBirth = item.DayOfBirth;
                    result.NationalId = item.NationalId;
                    result.MobilePhone = item.MobilePhone;
                    result.Phone1 = item.Phone1;
                    result.AmountLoan = item.AmountLoan;
                    result.Assignee = assignedId;
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
                    result.NoteRel = item.NoteRel;
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
                    result.UpdateAt = DateTime.Now;
                    result.UpdatedBy = userLogin.Id;
                    await UpdateSkipData(result);
                }

            }

            return true;

        }


        public async Task<bool> DeleteCampagnFile(List<string> DataDelete, string campagnId)
        {
            return await _unitOfWork.CampagnProfileRe.DeleteMutipleCam(DataDelete, campagnId);
        }
        public Task<bool> AssignedTask(string profileId, string userId)
        {
            return _unitOfWork.CampagnProfileRe.AssignedTask(profileId, userId);
        }

        public async Task<CampangeProfileInforReponse> GetInfo(string id)
        {
            var result = await _unitOfWork.CampagnProfileRe.GetById(id);
            var reponse = new CampangeProfileInforReponse()
            {

            };
            if (result == null)
            {
                return reponse;
            }

            
            var allImpactHistory = await _unitOfWork.ImpactRe.GetALl(
                new ImpactHistorySerarchRequest() { ProfileId = id, NoAgreement = result.NoAgreement }
            );

            var allSkipData = await _unitOfWork.SkipRe.GetALl(
                new SkipInfoSerarchRequest()
                {
                    NoAgreement = result.NoAgreement
                }

                );
            var listReason = await _unitOfWork.MasterRe.GetALl(
                new MaterDataRequest()
                { GroupStatus = "4" }
            );
            var listUser = await _unitOfWork.Employees.GetALl(new EmployeeSearchRequest() { });
            reponse.ListUser = listUser.Data;
            reponse.Result = result;
            var statusText = "Chưa rõ trạng thái";
            if (allImpactHistory !=null )
            {
                reponse.ListHistory = allImpactHistory.Data;
            } 
          

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
            reponse.ListSkipNew = allSkipData;
            return reponse;
        }

        public async Task<MasterDataReponse> GetAllReason()
        {
            var result = await _unitOfWork.MasterRe.GetALl(
                new MaterDataRequest()
                { GroupStatus = "4" }
            );
            return result;
        }
        public async Task<bool> HandleCase(CampaignProfile_caseRequest request)
        {
            return await _unitOfWork.CampagnProfileRe.HanldleCase(
                request.Id,
                request.ResetCase,
                request.Skipp
            );
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

        public async Task<bool> ResetCase(string campagnCase = "11")
        {
            var allCampang = await _unitOfWork.CampagnProfileRe.GetALLAsiggnee(
                new GetAllProfileByCampang()
                {
                    Id = campagnCase,
                    Skipp = 0,
                    Limit = 20000
                }
            );
            foreach (var item in allCampang)
            {
                item.Status = 20;
                if (item.Skipp == true)
                {
                    continue;
                }
                item.UpdateAt = DateTime.Now;
                item.Assignee = null;

                await _unitOfWork.CampagnProfileRe.Update(item);
            }
            return true;
        }

        public async Task<CampagnProfile> GetProfileByNoAgree(string noAgree)
        {
            return await _unitOfWork.CampagnProfileRe.GetByNoAgreement(noAgree);
        }

        public async Task<CampagnProfile> GetProfileByNoCMND(string noNational)
        {
            return await _unitOfWork.CampagnProfileRe.GetProfileByNoCMND(noNational);
        }

        public async Task<CampagnProfile> GetProfileByNoCMNDv2(string noNational, string cmnd, string phoneNumber)
        {
            return await _unitOfWork.CampagnProfileRe.GetProfileByNoCMNDv2(noNational, cmnd, phoneNumber);
        }

        public async Task<List<CampagnProfile>> GetAllInfoSkipp(string noNational)
        {
            return await _unitOfWork.CampagnProfileRe.GetAllInfoSkipp(noNational);

        }
    }
}
