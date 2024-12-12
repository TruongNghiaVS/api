using Quartz;
namespace sendEmail.sendmail
{
    public class UploadFileReport : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sctpMail = new ServiceSFCP();
            await sctpMail.UploadFileToRemoteFolder();
            Console.WriteLine("Completed upload send at:" + DateTime.Now.ToString("yyyy/MM/dd : HH:mm:ss"));
        }
    }
}
