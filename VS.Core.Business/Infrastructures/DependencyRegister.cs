using Microsoft.Extensions.DependencyInjection;
using VS.Core.Business.Interface;
using VS.Core.Repository.Infrastructures;

namespace VS.Core.Business.Infrastructures
{

    public static class DependencyRegister
    {
        public static void RegisterBusiness(this IServiceCollection services)
        {
            //repository
            services.RegisterRepository();
            //business
            services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IMasterDataBussiness, MasterDataBusiness>();
            services.AddScoped<IGroupResonBussiness, GroupResonBusiness>();
            services.AddScoped<ICampagnBussiness, CampagnBusiness>();
            services.AddScoped<IImpactHistoryBussiness, ImpactHistoryBusiness>();
            services.AddScoped<IMasterDataNewBussiness, MasterDataNewBussiness>();
            services.AddScoped<IGroupEmpBussiness, GroupEmpBussiness>();
            services.AddScoped<ILoginReportBussiness, LoginReportBusiness>();
            services.AddScoped<IReportBussiness, ReportBussiness>();
            services.AddScoped<ICallLogBussiness, CallLogBussiness>();
            services.AddScoped<ISmsBussiness, SmsBussiness>();
            services.AddScoped<IHandleReportBussiness, HandleReportBussiness>();
            services.AddScoped<IReportTalkTimeGroupByDayBussiness, ReportTalkTimeGroupByDayBussiness>();
            services.AddScoped<ILineManagementBussiness, LineManagementBusiness>();
            services.AddScoped<IDpdManagementBussiness, DpdManagementBusiness>();
            services.AddScoped<IPackageManagementBussiness, PackageManagementBusiness>();
            services.AddScoped<IViewRecordingBussiness, ViewRecordingBussiness>();
            services.AddScoped<ISkipInfoBussiness, SkipInfoBussiness>();

        }



    }

}
