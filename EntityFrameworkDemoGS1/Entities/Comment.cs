namespace EntityFrameworkDemoGS1.Entities;

public class Comment
{
    public int Id { get; set; }
    public string? Content { get; set; }
    public bool Recommend { get; set; }
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;
}
