using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgresiveBlog.Domain.Aggregates.Post;

namespace ProgresiveBlog.Dal.EntityConfiguration
{
    internal class PostInteractionConfig : IEntityTypeConfiguration<PostInteraction>
    {
        public void Configure(EntityTypeBuilder<PostInteraction> builder)
        {
            builder.HasKey(pi => pi.PostInteractionId);
        }
    }
}
