using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkDemoGS1.Entities;

public class Genre
{
    public int Id { get; set; }

    //[StringLength(maximumLength: 150)]
    public String Name { get; set; } = null!;

    public HashSet<Movie> Movies { get; set; } = new HashSet<Movie>();
}
