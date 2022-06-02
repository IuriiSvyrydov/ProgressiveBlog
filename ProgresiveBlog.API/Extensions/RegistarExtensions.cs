using ProgresiveBlog.API.Registars;

namespace ProgresiveBlog.API.Extensions
{
    public static class RegistarExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder,Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationBuilderRegistar>(scanningType);

            foreach (var registrar in registrars)
            {
                registrar.RegistarServices(builder);
            }
        }
        public static void RegisterPipelineComponents(this WebApplication app,Type scanningType)
        {
            var registrars = GetRegistrars<IWebApplicationRegistar>(scanningType);
            foreach (var registrar in registrars)
            {
                registrar.RegisterPipeLineComponents(app);
            }
        }
        public static IEnumerable<T> GetRegistrars<T>(Type scanningType) where T : IRegistars
        {
            return scanningType.Assembly.GetTypes()
                .Where(t => t.IsAssignableTo(typeof(T)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<T>();
        }
    }
}
