using Fitness.Common.Communication.Email;

namespace Identity.API.Configurations
{
    public static class ApiConfigurations
    {
        public static WebApplicationBuilder ApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.GetOptions();
            builder.Services.GetConfigurationLayers(); 
            builder.Services.AddEndpointsApiExplorer(); 
            builder.Services.GetJwtBearer(builder.Configuration);
            builder.Services.AddControllers().AddFluentValidation();
            builder.Services.GetSwaggerConfiguration();

            return builder;
        }

        private  static WebApplicationBuilder GetOptions(this WebApplicationBuilder builder)
        { 
            builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("EmailOptions"));
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

            return builder;
        }
    }
}
