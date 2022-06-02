namespace ProgresiveBlog.API.Registars
{
    public class MvcRegistar : IWebApplicationBuilderRegistar
    {
        public void RegistarServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(config => {
                config.Filters.Add(typeof(BlogExceptionFilter));
            });
            builder.Services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            builder.Services.AddVersionedApiExplorer(config =>
            {
                config.GroupNameFormat = "'v'VVV";
                config.SubstituteApiVersionInUrl = true;
            });
            builder.Services.AddEndpointsApiExplorer();

        }
    }
}
