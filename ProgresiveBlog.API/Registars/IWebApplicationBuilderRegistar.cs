namespace ProgresiveBlog.API.Registars
{
    public interface IWebApplicationBuilderRegistar: IRegistars
    {
        public void RegistarServices(WebApplicationBuilder builder);

    }
}
