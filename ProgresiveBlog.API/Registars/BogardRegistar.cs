namespace ProgresiveBlog.API.Registars
{
    public class BogardRegistar: IWebApplicationBuilderRegistar
    {
        public void RegistarServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program),typeof(GetUAllUserProfile));
            builder.Services.AddMediatR(typeof(Program), typeof(GetUAllUserProfile));
            
            
        }
    }
}
