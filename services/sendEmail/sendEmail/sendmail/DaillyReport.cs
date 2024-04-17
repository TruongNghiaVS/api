using System;
using System.Net.WebSockets;
using Quartz;
using sendEmail;
namespace sendEmail.sendmail
{
    public  class DaillyReport : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sendmailObject = new Sendmail();
            await sendmailObject.Send();
            Console.WriteLine("Completed send at:" + DateTime.Now.ToString("yyyy/MM/dd : HH:mm:ss" ));
            
        }
    }
}
