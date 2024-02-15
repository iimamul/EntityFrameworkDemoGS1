using EntityFrameworkDemoGS1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System.Reflection;

namespace EntityFrameworkDemoGS1;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //to get all entity configurations from Configurations folder
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //modelBuilder.Entity<Comment>().Property(c => c.Content).HasMaxLength(500);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().HaveMaxLength(150);
    }


    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<MovieActor> MovieActors { get; set; }

}
