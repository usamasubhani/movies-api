namespace Homitag.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Duration { get; set; }
        public double Rating { get; set; }
        public ICollection<Genre> Genres { get; set; }
    }

}
