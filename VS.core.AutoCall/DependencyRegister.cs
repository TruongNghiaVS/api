using Microsoft.Extensions.DependencyInjection;

namespace VS.core.AutoCall
{

    public static class DependencyRegister
    {
        public static void RegisterAutoCall(this IServiceCollection services)
        {
            //repository
            services.AddScoped<IServiceBussiness, ServiceBussiness>();
            services.AddScoped<IAutoBussiness, AutoBussiness>();

            //business


        }



    }

}
