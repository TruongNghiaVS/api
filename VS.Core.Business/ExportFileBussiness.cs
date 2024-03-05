using VS.core.Request;
using VS.Core.Business.Interface;
using VS.Core.Repository.baseConfig;
using IronXL;
namespace VS.Core.Business
{

    
    public class ExportFileBussiness : IExportFileBussiness
    {
        private readonly IUnitOfWork _unitOfWork1;
        public ExportFileBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }

        public async  Task<bool> HandleExportFile(CampagnProfileExportRequest request)
        {
            WorkBook workBook = WorkBook.Create();


            int pageBatch = 0;
            WorkBook xlsWorkBook = WorkBook.Create(ExcelFileFormat.XLS);

            // Create a blank WorkSheet
            WorkSheet workSheet = workBook.CreateWorkSheet("testExportFile");

            


            bool isContinue = true;
            do
            {
                pageBatch++;
              
                var requestQuery = new CampagnProfileExportRequest()
                {
                    CampaignId = request.CampaignId,
                    From = request.From,
                    To = request.To,
                    Limit = 500,
                    Page = pageBatch,
                    VendorId = request.VendorId
                };
               var resultData = await _unitOfWork1.ExportFile.GetAllCampagnProfile(requestQuery);
                isContinue = (resultData.NumberRecord > 0);

                //lưu file excel
                var data = resultData.Data;
                foreach (var item in data)
                {
                    workSheet["A1"].Value = "pageBatch";
                    workSheet["A1"].Style.WrapText = true;
                    workSheet["A2"].BoolValue = true;
                    workSheet["A2"].Style.BottomBorder.Type = IronXL.Styles.BorderType.Double;

                }

            } 
            while (isContinue);
            workBook.SaveAs("sample.xlsx");
            return true;

        }

    }
}
