namespace sendEmail
{
    using Microsoft.Extensions.Hosting;
    using Quartz;
    using sendEmail.sendmail;


    internal class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder()
            .ConfigureServices((cxt, services) =>
            {
                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();
                    var jobKey = new JobKey("DaillySendMailReport");
                    q.AddJob<DaillySendMailReport>(opts => opts.WithIdentity(jobKey));
                    q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("DaillySendMailReportJob-trigger")
                .WithCronSchedule("0 1,48 8-15 ? * MON,TUE,WED,THU,FRI,SAT *")
                );

                }
                );
                // services.AddQuartz(q =>
                // {
                //     q.UseMicrosoftDependencyInjectionJobFactory();
                //     var jobKey = new JobKey("DowloadFilePaymentReport");
                //     q.AddJob<DowloadFilePaymentReport>(opts => opts.WithIdentity(jobKey));
                //     q.AddTrigger(opts => opts
                // .ForJob(jobKey)
                // .WithIdentity("DowloadFilePaymentReportJob-trigger")
                // .WithCronSchedule("0 0,30 8-14 ? * MON,TUE,WED,THU,FRI,SAT *")
                // );

                // }
                //);
                //services.AddQuartz(q =>
                //{
                //    q.UseMicrosoftDependencyInjectionJobFactory();
                //    var jobKey = new JobKey("UploadFileReport");
                //    q.AddJob<UploadFileReport>(opts => opts.WithIdentity(jobKey));
                //    q.AddTrigger(opts => opts
                //.ForJob(jobKey)
                //.WithIdentity("UploadFileReporttrigger")
                //.WithCronSchedule("0 20,45 8-12 ? * MON,TUE,WED,THU,FRI,SAT *")
                //);

                //}
                //);

                services.AddQuartzHostedService(opt =>
                {
                    opt.WaitForJobsToComplete = true;
                });
            }).Build();
            await builder.RunAsync();
        }
    }
}
