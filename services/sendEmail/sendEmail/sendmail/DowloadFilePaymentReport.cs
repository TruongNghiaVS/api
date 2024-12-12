using Quartz;
namespace sendEmail.sendmail
{
    public class DowloadFilePaymentReport : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sctpMail = new ServiceSFCP();
            await sctpMail.DowloadFilePayment();
            Console.WriteLine("Completed  dowload send at:" + DateTime.Now.ToString("yyyy/MM/dd : HH:mm:ss"));
        }
    }
}
