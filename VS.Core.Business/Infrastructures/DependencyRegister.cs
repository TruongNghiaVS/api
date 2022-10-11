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


        }
    }

}
