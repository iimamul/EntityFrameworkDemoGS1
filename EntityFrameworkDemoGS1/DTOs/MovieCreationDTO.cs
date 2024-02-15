namespace EntityFrameworkDemoGS1.DTOs;

public class MovieCreationDTO
{
    public string Title { get; set; } = null!;
    public bool InTheaters { get; set; }
    public DateTime ReleaseDate { get; set; }
    public List<int> Genres { get; set; } = new List<int>();
    public List<MovieActorCreationDTO> MovieActors { get; set; } = new List<MovieActorCreationDTO>();
}

public class MovieActorCreationDTO
{
    public int ActorId { get; set; }
    public string Character { get; set; } = null!;
}
