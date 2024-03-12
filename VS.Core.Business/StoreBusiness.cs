using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;


namespace VS.Core.Business
{
    public class StoreBusiness : BaseBusiness, IStoreBussiness
    {
        public StoreBusiness(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Task<int> Add(Campagn entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckDuplicate(string code)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Campagn entity)
        {
            throw new NotImplementedException();
        }

         public Task<GetAllProfileByCampangReponse> GetALlProfileByCampaign(
      GetAllProfileByCampang request )
        {
            return _unitOfWork.StoreRe.GetALlProfileByCampaign(request);
        }


        public async Task<bool> HandleImport(List<StoreSkipInfo> dataImport, Account userLogin)
        {
            if(dataImport.Count < 1)
            {
                return true ;
            }    
            foreach (var item in dataImport)
            {
                 await _unitOfWork.StoreRe.AddOrUpdateSkip(item);
            }
            return true;
        }

        public async Task<StoreLookingReponse> GetInfo(string noAgree)
        {
            var itemReponse = new StoreLookingReponse();
            itemReponse.Result = await _unitOfWork.StoreRe.GetByNoAgree(noAgree);
            itemReponse.ListSkipNew = await _unitOfWork.StoreRe.GetAllSkip(noAgree);
            itemReponse.listHistory = await _unitOfWork.ImpactRe.GetAllHistoryBYNoAgree(noAgree);
          

            return itemReponse;
        }

        public Task<Campagn> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsyn(Campagn entity)
        {
            throw new NotImplementedException();
        }

        private DateTime? ReadvalueDateExcel2(dynamic datetimeInput)
        {

            if(datetimeInput == null)
            {
                return null;
            }    
            string textDate = datetimeInput.ToString();
            try
            {
                var dtp = DateTime.Parse(textDate);
                return dtp;
             

            }
            catch (Exception)
            {
                try
                {
                    return DateTime.ParseExact(textDate, "MM/dd/yyyy", null);

                }
                catch (Exception)
                {
                    try
                    {
                        return DateTime.ParseExact(textDate, "dd/MM/yyyy", null);

                    }
                    catch (Exception)
                    {

                        return null;
                    }

                   
                }

               
            }


        }
        private string ReadValuestring(dynamic textConvert)
        {
            if(textConvert == null )
            {
                return string.Empty;
            }  
            return textConvert.ToString();


        }
        public async Task<bool> RunLargefile(string pathFile ="")
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;
            List<StoreSkipInfo> profileList = new List<StoreSkipInfo>();

            for (int i = 2; i <= xlRange.Rows.Count; i++)
            {
                var nationalId = ReadValuestring(xlRange.Cells[i, 1].Value2);
                var typeCustomer = ReadValuestring(xlRange.Cells[i, 2].Value2);
                var name = ReadValuestring(xlRange.Cells[i, 3].Value2);
                var relation = ReadValuestring(xlRange.Cells[i, 4].Value2) ;
                var dob = ReadvalueDateExcel2(xlRange.Cells[i, 5].Value2);
                var cmnd = ReadValuestring(xlRange.Cells[i, 6].Value2);
                var phoneNumber = ReadValuestring( xlRange.Cells[i, 7].Value2);
                var address = ReadValuestring( xlRange.Cells[i, 8].Value2);
                var address2 = ReadValuestring( xlRange.Cells[i, 9].Value2);
                var fromDay = ReadValuestring( xlRange.Cells[i, 10].Value2);
                var toDay = ReadValuestring( xlRange.Cells[i, 11 ].Value2);
                var timejoin = ReadValuestring( xlRange.Cells[i, 12].Value2);
                var position = ReadValuestring( xlRange.Cells[i, 13].Value2);
                var salaryDe = ReadValuestring( xlRange.Cells[i, 14].Value2);
                var hsl = ReadValuestring( xlRange.Cells[i, 15].Value2);
                var companyName = ReadValuestring( xlRange.Cells[i, 16].Value2);
                var WorkPlace = ReadValuestring( xlRange.Cells[i, 17].Value2);
                var WorkContact = ReadValuestring( xlRange.Cells[i, 18].Value2);
                var WhoseCompany = ReadValuestring( xlRange.Cells[i, 19].Value2);
                var PhoneCompany = ReadValuestring( xlRange.Cells[i, 20].Value2);
                var PersonContact = ReadValuestring( xlRange.Cells[i, 21].Value2);
                var personContactPhone = ReadValuestring( xlRange.Cells[i, 22].Value2);
                var personContactEmail = ReadValuestring( xlRange.Cells[i, 23].Value2);

                var skipInfo = new StoreSkipInfo()
                {
                    NationalId = nationalId,
                    TypeCustomer = typeCustomer,
                    Address = address,
                    Address2 = address2,
                    CMND =  cmnd,
                    CompanyName = companyName,
                    Dob = dob,
                    FromDay = fromDay,
                    ToDay   = toDay,
                    Hsl = hsl,
                    Name = name,
                    PersonContact = PersonContact,
                    PhoneCompany = PhoneCompany,
                    PhoneNumber =phoneNumber,
                    Position =position,
                    Relation =relation,
                    SalaryDe = salaryDe,
                    Timejoin = timejoin,
                    WhoseCompany= WhoseCompany,
                    WorkContact =WorkContact,
                    WorkPlace = WorkPlace,
                    PersonContactPhone = personContactPhone,
                    PersonContactEmail = personContactEmail,
                    CreatedBy = "1",
                    UpdatedBy = "1",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Deleted = false,
                    NoAgree = ""

                };
                profileList.Add(skipInfo);
                if (i % 5000 == 0)
                {
                    await HandleImport(profileList, null);
                    profileList.Clear();
                }

            }

            if(profileList.Count() >0)
            {
                await HandleImport(profileList, null);
            }    
            xlWorkbook.Close();
            xlApp.Quit();
            return true;
        }


    }
}
