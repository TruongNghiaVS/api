using Microsoft.Extensions.DependencyInjection;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;
using VS.Core.Repository.Report;

namespace VS.Core.Repository.Infrastructures
{
    public static class DependencyRegister
    {
        public static void RegisterRepository(this IServiceCollection services)
        {

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMasterDataRepository, MasterdataRepository>();
            services.AddScoped<IGroupReasonRepository, GroupReasonRepository>();
            services.AddScoped<ICampagnRepository, CampagnRepository>();
            services.AddScoped<IProfileCampagnRepository, ProfileCampagnRepository>();
            services.AddScoped<IImpactHistoryRepository, ImpactHistoryRepository>();
            services.AddScoped<IMasterDataNewRepository, MasterdataNewRepository>();
            services.AddScoped<IGroupEmployRepository, GroupEmployRepository>();
            services.AddScoped<IloginReportRepository, LoginReportReposiotry>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ISmsMessageRepository, SmsMessageRepository>();
            services.AddScoped<ICallLogRepository, logCallRepository>();
            services.AddScoped<IReportTalkTimeRepository, ReportTalkTimeRepository>();
            services.AddScoped<IReportTalkTimeGroupByDay, ReportTalkTimeGroupByDayRepository>();
            services.AddScoped<ILineRepository, LineRepository>();
            services.AddScoped<IDpdRepository, DpdRepository>();

            services.AddScoped<IPackageRepository, PackageRepository>();

            services.AddScoped<IViewRecordingRepository, ViewRecordingRepository>();

            services.AddScoped<ISkipInfoRepository, SkipInfoRepository>();
            services.AddScoped<IWorkplaceNotedRepository, WorkplaceNotedRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();



        }


    }
}
