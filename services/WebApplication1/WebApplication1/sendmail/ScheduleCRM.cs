using System;
using System.Net.WebSockets;
using Quartz;
using sendEmail;
namespace Services.sendmail
{
    public  class ScheduleCRM : IJob
    {

        public async Task Execute(IJobExecutionContext context)
        {
            var sendmailObject = new ScheduleToolCRM();
            await sendmailObject.Run();
            Console.WriteLine("Mail dailly report.");
            
        }
    }
}
