namespace Identity.API.Configurations
{
    public static class ConfigurationLayers
    {
        public static IServiceCollection GetConfigurationLayers(this IServiceCollection services)
        {
            //Application
            services.GetValidators();
            services.GetDependencyInjectionApplication();

            //Infrastructure
            services.GetDependencyInjectionInfrastructure();

            return services;
        }
    }
}
