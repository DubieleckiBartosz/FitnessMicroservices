namespace Identity.Application.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection GetJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        //OnChallenge = async (context) =>
                        //{

                        //    if (context.AuthenticateFailure != null)
                        //    {
                        //        var error = string.IsNullOrEmpty(context.ErrorDescription) ? context.AuthenticateFailure?.Message : context.ErrorDescription;
                        //        context.HandleResponse();
                        //        context.Response.ContentType = "application/json";
                        //        context.Response.StatusCode = 401;
                        //        await context.Response.WriteAsJsonAsync(new {Message = $"401 Not authorized: {error}" });
                        //    }
                        //}

                        OnChallenge = async (context) =>
                        {
                            if (context.AuthenticateFailure != null)
                            {
                                var error = string.IsNullOrEmpty(context.ErrorDescription)
                                    ? context.AuthenticateFailure?.Message
                                    : context.ErrorDescription;
                                context.HandleResponse();
                                context.Response.ContentType = "application/json";
                                var statusCode = context?.AuthenticateFailure is SecurityTokenExpiredException
                                    ? 403
                                    : 401;
                                if (context != null)
                                {
                                    context.Response.StatusCode = statusCode;
                                    await context.Response.WriteAsJsonAsync(new
                                    {
                                        ErrorMessage = statusCode == 403
                                            ? $"403 Expired token: {error}"
                                            : $"401 Not authorized: {error}"
                                    });
                                }
                            }
                        }
                    };
                });
            return services;
        }
    }
}
