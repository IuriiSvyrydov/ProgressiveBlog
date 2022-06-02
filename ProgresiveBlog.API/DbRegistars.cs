
namespace ProgresiveBlog.API
{
    public class DbRegistars: IWebApplicationBuilderRegistar
    {
        public void RegistarServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddDbContext<PostDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration
                        .GetConnectionString("DefaultConnection"));
                    
                });
            builder.Services.AddIdentityCore<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<PostDbContext>();


        }
    }
}
