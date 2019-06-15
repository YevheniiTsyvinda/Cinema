namespace Cinema.Models.Tickets
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public float Rating { get; set; }
        public Type[] Type { get; set; }
        public Genre[] Genre { get; set; }
        public int MinAge { get; set; }
        public string ImageUrl { get; set; }
    }
}