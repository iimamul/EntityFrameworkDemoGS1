using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EntityFrameworkDemoGS1.Entities.Configurations
{
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //builder.Property(a => a.Name).HasMaxLength(150);
            builder.Property(a => a.DateOfBirth).HasColumnType("date");
            builder.Property(a => a.Fortune).HasPrecision(18, 2);

        }
    }
}
