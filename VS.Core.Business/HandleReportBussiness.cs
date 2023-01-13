using VS.Core.Business.Interface;
using VS.Core.dataEntry.User;
using VS.Core.Repository.baseConfig;

namespace VS.Core.Business
{
    public class HandleReportBussiness : IHandleReportBussiness
    {

        private readonly IUnitOfWork _unitOfWork1;
        public HandleReportBussiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork1 = unitOfWork;
        }


        public async Task<int> CalTalkingTime()
        {
            //get all cdr having answere

            var allcdrHaving = await _unitOfWork1.ReportTalkTimeRepository.HandlelFileRecording(
            new core.Request.HandlelFileRecordingRequest()
            {

            }
            );
            var data = allcdrHaving;

            if (data == null)
            {
                return 0;
            }

            foreach (var item in data)
            {

                var processingInfo = await _unitOfWork1.ReportTalkTimeRepository.GetInfomationRecording(item.Linkedid);


                var reportTalkTime = new ReportTalkTime()
                {
                    CallDate = item.CallDate,
                    LineCode = item.LineCode,
                    PhoneLog = item.PhoneLog,


                };
                if (processingInfo != null)
                {
                    reportTalkTime.Duration = processingInfo.Duration;
                    reportTalkTime.FileRecording = processingInfo.File;

                }
                else
                {

                    reportTalkTime.Duration = 0;
                    reportTalkTime.FileRecording = "";
                }

                await _unitOfWork1.ReportTalkTimeRepository.AddAsync(reportTalkTime);
            }


            return await Task.FromResult(0);


        }



    }
}
