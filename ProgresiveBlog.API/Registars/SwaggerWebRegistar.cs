namespace ProgresiveBlog.API.Registars
{
    public class SwaggerWebRegistar : IWebApplicationRegistar
    {
        public void RegisterPipeLineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToString());
                }
            });
        }
    }
}
