using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EntityFrameworkDemoGS1.Entities.Configurations;

public class MovieConfig : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        //builder.Property(m => m.Title).HasMaxLength(150);
        builder.Property(m => m.ReleaseDate).HasColumnType("date");
    }
}
