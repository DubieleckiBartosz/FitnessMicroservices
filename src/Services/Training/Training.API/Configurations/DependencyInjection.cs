using Training.API.Repositories;
using Training.API.Repositories.Interfaces;

namespace Training.API.Configurations
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder RegisterTypes(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IWrapperRepository, WrapperRepository>(); 

            return builder;
        }
    }
}
