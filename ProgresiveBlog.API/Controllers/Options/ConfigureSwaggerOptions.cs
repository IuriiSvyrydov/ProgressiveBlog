
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ProgresiveBlog.API.Controllers.Options
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>

    {
        private readonly IApiVersionDescriptionProvider _provider;
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach(var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName,CreateVersionInfo(description));
            }

            var scheme = GetJwtSecurityScheme();
            options.AddSecurityDefinition(scheme.Reference.Id,scheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {scheme,new string[0]}
            });
        }
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Progresive Blog",
                Version = description.ApiVersion.ToString()
            };
            if(description.IsDeprecated)
            {
                info.Description = "This Api version has been depacted ";
            }
            return info;
        }

        private OpenApiSecurityScheme GetJwtSecurityScheme()
        {
            return new OpenApiSecurityScheme
            {
                Name = "Jwt Authentication",
                Description = "Provide a Jwt Bearer",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
        }

    }
}
