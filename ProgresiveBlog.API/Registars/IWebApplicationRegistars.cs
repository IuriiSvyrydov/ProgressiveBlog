namespace ProgresiveBlog.API.Registars
{
    public interface IWebApplicationRegistar : IRegistars
    {
        public void RegisterPipeLineComponents(WebApplication app);
    }
}
