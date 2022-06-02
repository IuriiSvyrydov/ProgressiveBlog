namespace ProgresiveBlog.API.Registars
{
    public class SwaggerRegistar : IWebApplicationBuilderRegistar
    {
        public void RegistarServices(WebApplicationBuilder builder)
        {
            builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
            builder.Services.AddSwaggerGen();
        }
    }
}
