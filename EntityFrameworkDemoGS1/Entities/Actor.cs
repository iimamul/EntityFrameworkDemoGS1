namespace EntityFrameworkDemoGS1.Entities
{
    public class Actor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Fortune { get; set; }
        public DateTime DateOfBirth { get; set; }
        public HashSet<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();
    }
}
