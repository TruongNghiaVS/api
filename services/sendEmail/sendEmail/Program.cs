using Microsoft.Extensions.Hosting;
using Quartz;
using sendEmail.sendmail;
namespace sendEmail
{


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
               var jobKey = new JobKey("sendReportJob");
               q.AddJob<DaillyReport>(opts => opts.WithIdentity(jobKey));
                   q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("HelloWorldJob-trigger")
                .WithCronSchedule("0 30 8 ? * MON,TUE,WED,THU,FRI *")
                .WithCronSchedule("0 30 17 ? * SAT *")
                );
               
             });
            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });
           }).Build();


            //var schedulerFactory = builder.Services.GetService(typeof(IScheduleBuilder));
                
            //var scheduler = await schedulerFactory.

            //// define the job and tie it to our HelloJob class
            //var job = JobBuilder.Create<DaillyReport>()
            //    .WithIdentity("sendReport", "daillyReport")
            //    .Build();

            //// Trigger the job to run now, and then every 40 seconds
            //var trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInSeconds(10)
            //        .RepeatForever())
            //    .Build();

            //await scheduler.ScheduleJob(job, trigger);
            //// will block until the last running job completes
            await builder.RunAsync();
            
        }


    }
}
