using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkDemoGS1.Entities.Configurations;

public class MovieActorConfig : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasKey(ma => new { ma.MovieId, ma.ActorId });
    }
}
