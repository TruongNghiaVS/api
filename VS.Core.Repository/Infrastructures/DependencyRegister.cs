using Microsoft.Extensions.DependencyInjection;
using VS.Core.Repository.baseConfig;
using VS.Core.Repository.Interface;

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
            services.AddTransient<IUnitOfWork, UnitOfWork>();

        }


    }
}
