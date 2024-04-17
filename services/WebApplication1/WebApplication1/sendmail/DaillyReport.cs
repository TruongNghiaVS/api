using System;
using System.Net.WebSockets;
using Quartz;
using sendEmail;
namespace Services.sendmail
{
    public  class DaillyReport : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sendmailObject = new Sendmail();
            await sendmailObject.Send();
            Console.WriteLine("Mail dailly report.");
            
        }
    }
}
