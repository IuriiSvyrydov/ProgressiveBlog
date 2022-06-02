using ProgresiveBlog.Application.Identity.Services;

namespace ProgresiveBlog.API.Registars
{
    public class ApplicationLayerRegister:IWebApplicationBuilderRegistar
    {
        public void RegistarServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IdentityService>();
        }
    }
}
