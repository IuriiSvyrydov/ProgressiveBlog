using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgresiveBlog.Domain.Aggregates.User;

namespace ProgresiveBlog.Dal.EntityConfiguration
{
    internal class BasicInfoConfig : IEntityTypeConfiguration<BasicInfo>
    {
        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
            builder.HasNoKey();
        }
    }
}
