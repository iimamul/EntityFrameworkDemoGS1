namespace EntityFrameworkDemoGS1.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public HashSet<Comment> Comments { get; set; } = new HashSet<Comment>();

        public HashSet<Genre> Genres { get; set; } = new HashSet<Genre>();

        public HashSet<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();
    }
}
