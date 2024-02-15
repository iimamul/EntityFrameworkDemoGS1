using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkDemoGS1.Entities.Configurations;

public class GenreConfig : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        //builder.HasKey(k => k.Id);
        //builder.Property(g => g.Name).HasMaxLength(150);
        var scienceFiction = new Genre { Id = 6, Name = "Science Fiction" };
        var animation = new Genre { Id = 7, Name = "Animation" };

        builder.HasData(scienceFiction, animation);

        builder.HasIndex(b => b.Name).IsUnique();
    }
}
