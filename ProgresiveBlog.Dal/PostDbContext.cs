using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProgresiveBlog.Dal.EntityConfiguration;
using ProgresiveBlog.Domain.Aggregates.Post;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Dal
{
    public class PostDbContext: IdentityDbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext>options):base(options)
        {
            
        }

        public DbSet<UserProfile> UsersProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new BasicInfoConfig());
            builder.ApplyConfiguration(new IdentityUserLoginConfig());
            builder.ApplyConfiguration(new IdentityUserRoleConfig());
            builder.ApplyConfiguration(new IdentityUserTokenConfig());
            builder.ApplyConfiguration(new PostCommentConfig());
            builder.ApplyConfiguration(new PostInteractionConfig());
            builder.ApplyConfiguration(new UserProfileConfig());

        }
    }
}
