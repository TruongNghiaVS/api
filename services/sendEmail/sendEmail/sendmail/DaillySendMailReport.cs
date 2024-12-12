using Quartz;
namespace sendEmail.sendmail
{
    public class DaillySendMailReport : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sendmailObject = new Sendmail();
            await sendmailObject.sendemailreport();
            Console.WriteLine("Completed  send mail at:" + DateTime.Now.ToString("yyyy/MM/dd : HH:mm:ss"));

        }
    }
}
